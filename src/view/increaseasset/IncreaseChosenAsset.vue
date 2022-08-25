<template>
<div id="dbEpl" class="modal increase-modal-style chosen-modal-style">
    <div class="modal-layer"></div>
    <div class="modal-body">
        <div class="modal-body-title">
            <div class="modal-body-title_text">{{modalTitle}}</div>
            <div id="closeBtn" class="close-icon" @click="closeModalOnclick"></div>
        </div>
            <div class="input-row-wrap horizontal-style">
                <div class="content-menu-input">
                    <div class="input-icon">
                        <div class="input-icon1"></div>
                        <input maxlength="255" @keyup.enter="selectListByEnter" ref="txtSearch" v-model="search" type="text" class="input search-input" placeholder="Tìm kiếm theo mã, tên tài sản">
                    </div>
                </div>
            </div>
            <table class="content-table-title">
                <tr class="table-column-title">
                    <td class="column-increse1">
                        <input class="input-checkbox" type="checkbox" v-model="checkAll" @change="selectedAllRow()">
                    </td>
                    <td class="column-increse2" title="Số thứ tự">STT</td>
                    <td class="column-increse3" title="Số chứng từ">Mã tài sản</td>
                    <td class="column-increse4" title="Ngày chứng từ<">Tên tài sản</td>
                    <td class="column-increse5" title="Ngày ghi tăng">Bộ phận sử dụng</td>
                    <td class="column-increse6" title="Tổng nguyên giá">Nguyên giá </td>
                    <td class="column-increse7" title="Hao mòn năm">Hao mòn năm</td>
                    <td class="column-increse8" title="Giá trị còn lại">Giá trị còn lại</td>
                </tr>
            </table>
            <table id="tblListEmployees" class="content-table">
                <tr v-for="(fixedAsset,index) in assets" :key="fixedAsset.fixedAssetId" class="table-column-infor" @contextmenu="onContextMenu($event,fixedAsset)" :class="{'row-selected': checkboxArray[index]}" @click="clickOnRow(fixedAsset, index)">
                    <!-- v-model="checked" -->
                    <td class="column-increse1"><input class="input-checkbox" v-model="checkboxArray[index]" type="checkbox"></td>
                    <td class="column-increse2">{{recordStart+index}}</td>
                    <td class="column-increse3">{{fixedAsset.fixedAssetCode}}</td>
                    <td class="column-increse4">
                        <div :title="fixedAsset.fixedAssetName" class="tabletext-wrap">
                            {{fixedAsset.fixedAssetName}}
                        </div>
                    </td>
                    <td class="column-increse5">
                        <div :title="fixedAsset.departmentName" class="tabletext-wrap">
                            {{fixedAsset.departmentName}}
                        </div>
                    </td>
                    <td class="column-increse6">{{formatMoney(fixedAsset.cost)}}</td>
                    <td class="column-increse7">{{formatMoney(fixedAsset.depreciationValueYear)}}</td>
                    <td class="column-increse8">
                        {{formatMoney(fixedAsset.cost-fixedAsset.depreciationValueYear)}}
                    </td>
                </tr>
            </table>
            <table id="tblFooterPrice" class="content-table-title footer-table">
                <tr class="table-column-title table-footer">
                    <td class="column-style-fix12">
                        <div class="footer-table-text">
                            <div>Tổng số: </div>
                            <div class="footer-table-text-number"> {{totalRecord}} </div>
                            <div> bản ghi</div>
                        </div>
                    </td>
                    <td class="column-style-fix13">
                        <div class="combobox list-page-number-style">
                            <div class="combobox-selected">
                                <input autocomplete="off" title="Số ban ghi trên một trang" type="text" v-model="defaultPageSize" class="selected-item" @keydown.tab="tabOutCombobox" @focusin="totalPageSizeOnFocus" @keyup.enter="selectListByEnter" v-click-outside="onClickOutsidePageNumber">
                                <div class="input-icon3"></div>
                                <div class="combobox-list" :class="{'visible': isShowListPageSize}">
                                    <div class="combobox-items" @click="listTotalRecodsOnClick(pageSize1)">{{pageSize1}}</div>
                                    <div class="combobox-items" @click="listTotalRecodsOnClick(pageSize2)">{{pageSize2}}</div>
                                    <div class="combobox-items" @click="listTotalRecodsOnClick(pageSize3)">{{pageSize3}}</div>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td class="column-style-fix14">
                        <paginate v-model="page" :page-count="totalPage" :page-range="3" :margin-pages="2" :prev-text="''" :next-text="''" :container-class="'pagination'" :page-class="'page-item'" :prev-class="'prev'" :next-class="'next'">
                        </paginate>
                    </td>
                    <td class="column-style-fix15"></td>
                    <td class="column-style-fix11"></td>
                </tr>
            </table>
        <div class="modal-footer">
            <button id="btnSave" class="main-button rectangle-button modal-button-right" @click="saveOnClick">Đồng ý</button>
            <button id="btnCancle" class="main-button white-rectangle-button modal-button-left" @click="cancelOnClick">Hủy bỏ</button>
        </div>
    </div>
</div>
<HErrorMessage v-if="isShowErrorNotice" @closeNotice="showErrorNotice" :errorMessage="errorMessage" />
</template>

<style lang="css">
/* Write your own CSS for pagination */
/* Css của paginate */
@import url(../../css/base/pagination.css);

@import url(../../css/layout/increasetable.css);
</style>

<script>
import resources from '../../js/resource.js';
//sử dụng axioss
import axios from 'axios';
//Sử dụng thư viện paginate
import Paginate from "vuejs-paginate-next";


export default {
    name: "EmployeeDetail",
    emits: ["closeModal", "resetTable", "showSuccessMessage","closeSelected","closeChosen","getFixedAsset"],
    components: {
        paginate: Paginate,
    },

    data() {
        return {
            //Đặt thông số mặc định phân trang
            page: 1,
            totalPage: 20,
            recordStart: 1,
            totalRecord: 0,
            departmentDefaulName: '',
            fixedAssetCategoriesDefaulName: '',

            //Số lượng bản ghi trên một trang
            defaultPageSize: 20,
            pageSize1: 10,
            pageSize2: 20,
            pageSize3: 30,

            //Tính toán các giá trị tổng số
            totalQuantity: 0,
            totalAccumulated: 0,
            totalRemain: 0,
            totalPrice: 0,

            //Tạo title Modal
            modalTitle: '',

            //tạo đối tượng tài sản 
            fixedAsset: {},
            assets: [],

            //Đặt mặc định ẩn cảnh báo
            isShowErrorNotice: false,

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //Khai báo mảng checkboxArray
            checkboxArray: [],
            //Đặt mặc định ẩn highline row
            isHighLine: false,
            checked: false,
            isChecked: true,
            checkAll: false,

            //tạo mảng lưu id selected
            idSelected: [],

            //Tạo biến chứa error message
            errorMessage: [],

            //Tạo biến search
            search: '',

            //Mặc định ẩn lish
            isShowListPageSize: false,
        }
    },

    /**
     * Khai báo prop sử dụng bên ngoài
     */
    props: {
        editMode: {
            type: Number,
            default: 0, // thêm mới
            require: true
        }
    },

    methods: {

        /**
         * Tìm kiếm khi nhấn enter
         */
        selectListByEnter() {
            try {
                var me = this;
                //Ẩn danh sách
                me.isShowListPageSize = me.isHide;
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Format money
         * NDHoang (04/07/2022)
         */
        formatMoney(money) {
            try {
                //Làm tròn dữ liệu đầu vào
                money = String(Math.floor(money))
                return money.replace(/\B(?=(\d{3})+(?!\d))/g, resources.currency);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm chọn check box khi click vào dòng
         * NDHoang(22/06/2022)
         */
        clickOnRow(fixedAsset, index) {
            try {
                var me = this;
                if(me.checkboxArray[index] == me.isChecked) {
                    me.checkboxArray[index] = false;
                } else {
                    me.checkboxArray[index] = me.isChecked
                }
                 me.selectedRow(fixedAsset, index);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm high line row
         * NDHoang(22/06/2022)
         */
        selectedRow(fixedAsset, index) {
            try {
                var me = this
                //Điều kiện
                if (me.checkboxArray[index] == me.isChecked) {
                    //Bôi đậm dòng được check
                    me.isHighLine = me.isChecked;
                    //Gán lại giá trị selected
                    me.fixedAssetSelected = fixedAsset;
                    //Thêm id vào mảng
                    me.idSelected.push(fixedAsset);
                    if (me.idSelected.length == me.assets.length) {
                        me.checkAll = me.isShow;
                    }
                } else {
                    me.isHighLine = false;
                    //Xóa phần tử không check ra khỏi mảng
                    me.idSelected.splice(index, 1);
                    //Bỏ check all
                    me.checkAll = me.isHide;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm high line row
         * NDHoang(22/06/2022)
         */
        selectedAllRow() {
            try {
                var me = this
                //Làm rỗng mảng
                me.idSelected = [];
                //Bôi đậm dòng được check
                for (let i = 0; i < me.assets.length; i++) {
                    me.checkboxArray[i] = me.checkAll;
                    if (me.checkAll)
                        me.idSelected.push(me.assets[i]);
                    else
                        me.idSelected.pop();
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Click ra ngoài combobox
         * NDHoang(22/06/2022)
         */

        //Sự kiên click ra ngoài combobox page number
        onClickOutsidePageNumber() {
            try {
                var me = this;
                //Ẩn lish
                me.isShowListPageSize = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện onfocus input combobox
         * NDHoang (19/06/2022)
         */

        //Focus on combobox PageSize
        totalPageSizeOnFocus() {
            try {
                var me = this;
                //Hiện list
                me.isShowListPageSize = me.isShow;
            } catch (error) {
                console.log(error);
            }
        },

        //Click vào combobox pageSize items
        listTotalRecodsOnClick(Size) {
            try {
                var me = this;
                //Gán giá trị
                me.defaultPageSize = Size;
                //Gọi Api
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
                //Ẩn list
                me.isShowListPageSize = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Tắt modal
         * NDHoang(22/06/2022)
         */
        closeModal() {
            //Gọi đến phương thức của cha
            this.$emit("closeModal", this.isHide);
        },

        //Ẩn modal chosen asset
        hideChosen(){
            this.$emit("closeChosen", this.isHide);
        },

        //Hiển thị cảnh báo trước khi tắt
        closeModalOnclick() {
            try {
                var me = this
                //Xét trạng thái modal
                me.hideChosen();
            } catch (error) {
                console.log(error);
            }

        },

        //Hiển thị cảnh báo trước khi tắt
        cancelOnClick() {
            try {
                var me = this
                //Xét trạng thái modal
                me.hideChosen();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Show cảnh báo
         * NDHoang(22/06/2022)
         */
        //Cảnh báo lỗi
        showErrorNotice(errorNotice) {
            try {
                var me = this;
                //Hiện thông báo nếu errorNotice = true
                me.isShowErrorNotice = errorNotice;
            } catch (error) {
                console.log(error);
            }
        },

        //Cảnh báo thêm mới
        showPostNotice(postNotice) {
            try {
                var me = this;
                //Hiện thông báo nếu postNotice = true
                me.isShowPostNotice = postNotice;
            } catch (error) {
                console.log(error);
            }
        },

        //Cảnh báo sửa
        showPutNotice(putNotice) {
            try {
                var me = this;
                me.isShowPutNotice = putNotice;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Thao tác dữ liệu
         * NDHoang(22/06/2022)
         */
        saveOnClick() {
            var me = this;
            try {
                me.$emit("getFixedAsset",me.idSelected);
                me.hideChosen();
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Hàm gọi api phân trang
         * NDHoang(22/06/2022)
         */
        pagingApi(pageIndex, pageSize, search, departmentName, categoryName) {
            var me = this;
            //Gọi api phân trang
            axios.get(resources.url.host + `/api/v1/FixedAssets/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${search}&departmentName=${departmentName}&CategoryName=${categoryName}`)
                .then(function (res) {
                    me.assets = res.data.data;
                    me.totalPage = Math.ceil(res.data.totalRecord / pageSize);
                    me.totalRecord = res.data.totalRecord;
                    me.recordStart = res.data.recordStart;
                    //Gán lại các giá trị để tính giá trị mới
                    me.totalPrice = 0;
                    me.totalQuantity = 0;
                    me.totalAccumulated = 0;
                    me.totalRemain = 0;
                    //Tính toán lại các giá trị
                    for (const asset of me.assets) {
                        asset.depreciationRate = (1 / asset.lifeTime).toFixed(2);
                        asset.depreciationValueYear = asset.depreciationRate * asset.cost;
                        me.totalPrice += asset.cost;
                        me.totalQuantity += asset.quantity;
                        me.totalAccumulated += asset.depreciationValueYear;
                        me.totalRemain += asset.cost - asset.depreciationValueYear;
                    }
                })
                .catch(function (res) {
                    console.log(res);
                });
        },

    },

    /**
     * Xử lý dữ liệu số nhập vào
     * NDHoang(05/07/2022)
     */
    watch: {
        //Khi số trang thay đổi
        page(value) {
            var me = this;
            //Gọi Api paging
            me.pagingApi(value, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        },

        //Khi pageSize thay đổi
        defaultPageSize(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, value, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        },

        //Khi ô tìm kiếm thay đổi
        search(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, me.defaultPageSize, value, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        },
    },

    created() {
        var me = this;
        me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        //gán giá trị tài sản được chọn cho cho fixedAsset
        me.fixedAsset = me.fixedAssetSelected;
        //Load lại table

        //Xét trạng thái modal tương ứng
        me.modalTitle = resources.modalLicenseTitle.chosenAsset;
    },

    mounted() {
        var me = this;
        //Focus vào ô đầu tiên
        me.$refs.txtSearch.focus();
    }
}
</script>
