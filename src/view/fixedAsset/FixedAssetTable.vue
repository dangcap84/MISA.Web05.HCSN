<template>
<div class="content-menu">
    <div class="content-menu-input">
        <div class="input-icon">
            <div class="input-icon1"></div>
            <input id="txtSearch" autocomplete="off" v-model="search" type="text" class="input search-input" placeholder="Tìm kiếm theo tên">
        </div>
        <div class="combobox">
            <div class="combobox-selected">
                <div class="input-icon2"></div>
                <input @keyup.enter="selectListCategoriesByEnter(fixedAssetCategories)" @keyup.up="arrowListCategoriesUp(fixedAssetCategories)" @keyup.down="arrowListCategoriesDown(fixedAssetCategories)" autocomplete="off" type="text" v-model="fixedAssetCategoriesDefaulName" class="selected-item" @focusin="fixedAssetCategoriesComboboxOnFocus" @focusout="fixedAssetCategoriesOutFocus">
                <div class="input-icon3"></div>
                <div class="combobox-list" :class="{'visible': isShowListCategoriesAssets}">
                    <div v-for="(cat,index) in fixedAssetCategories" :key="cat.fixedAssetCategoryId" :class="{'ishover': index==arrowCategoriesCounter}" class="combobox-items" @click="listFixedAssetCategoriesOnClick(cat.fixedAssetCategoryName)">{{cat.fixedAssetCategoryName}}</div>
                </div>
            </div>
        </div>
        <div class="combobox">
            <div class="combobox-selected">
                <div class="input-icon2"></div>
                <input @keyup.enter="selectDepartmentsByEnter(departments)" @keyup.up="arrowDepartmentsUp(departments)" @keyup.down="arrowDepartmentsDown(departments)" autocomplete="off" type="text" v-model="departmentDefaulName" class="selected-item" @focusin="departmentsComboboxOnFocus">
                <div class="input-icon3"></div>
                <div class="combobox-list" :class="{'visible': isShowListDepartments}">
                    <div v-for="(dep,index) in departments" :key="dep.DepartmentId" :class="{'ishover': index==arrowDepartmentsCounter}" class="combobox-items" @click="listDepartmentsOnClick(dep.departmentName)">{{dep.departmentName}}</div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-menu-button">
        <button id="btnAdd" class="main-button rectangle-button" @click="addFixedAssetOnClick">
            +Thêm tài sản
        </button>
        <button class="export-button main-button square-button">
        </button>
        <button id="btnDelete" class="discard-button main-button square-button" @click="deleteOnClick">
        </button>
    </div>
</div>
<div class="content-table-wrap">
    <table class="content-table-title">
        <tr class="table-column-title">
            <td class="column-style-fix1">
                <input class="input-checkbox" type="checkbox" v-model="checkAll" @change="selectedAllRow()">
            </td>
            <td class="column-style-fix2" title="Số thứ tự">STT</td>
            <td class="column-style-fix3" title="Mã tài sản">Mã tài sản</td>
            <td class="column-style-fix4" title="Tên Tài sản">Tên tài sản</td>
            <td class="column-style-fix5" title="Loại Tài sản">Loại tài sản</td>
            <td class="column-style-fix6" title="Bộ phận sử dụng">Bộ phận sử dụng</td>
            <td class="column-style-fix7" title="Số lượng">Số lượng</td>
            <td class="column-style-fix8" title="Nguyên Giá">Nguyên giá</td>
            <td class="column-style-fix9" title="Hao mòn/Khấu hao (Nguyên giá * tỉ lệ hao mòn)">HM/KH lũy kế</td>
            <td class="column-style-fix10" title="Giá trị còn lại">Giá trị còn lại</td>
            <td class="column-style-fix11" title="Chức năng">Chức năng</td>
        </tr>
    </table>
    <table id="tblListEmployees" class="content-table">
        <tr v-for="(fixedAsset,index) in fixedAssets" :key="fixedAsset.fixedAssetId" class="table-column-infor" @dblclick="rowOnDbClick(fixedAsset)" :class="{'row-selected': checkboxArray[index]}">
            <!-- v-model="checked" -->
            <td class="column-style-fix1"><input class="input-checkbox" v-model="checkboxArray[index]" type="checkbox" @change="selectedRow(fixedAsset, index)"></td>
            <td class="column-style-fix2">{{recordStart+index}}</td>
            <td class="column-style-fix3">{{fixedAsset.fixedAssetCode}}</td>
            <td class="column-style-fix4">
                <div :title="fixedAsset.fixedAssetName" class="tabletext-wrap">
                    {{fixedAsset.fixedAssetName}}
                </div>
            </td>
            <td class="column-style-fix5">
                <div :title="fixedAsset.fixedAssetCategoryName" class="tabletext-wrap">
                    {{fixedAsset.fixedAssetCategoryName}}
                </div>
            </td>
            <td class="column-style-fix6">
                <div :title="fixedAsset.departmentName" class="tabletext-wrap">
                    {{fixedAsset.departmentName}}
                </div>
            </td>
            <td class="column-style-fix7">{{fixedAsset.quantity}}</td>
            <td class="column-style-fix8">{{formatMoney(fixedAsset.cost)}}</td>
            <td class="column-style-fix9">{{formatMoney((fixedAsset.lifeTime * fixedAsset.depreciationValueYear))}}</td>
            <td class="column-style-fix10">{{formatMoney(fixedAsset.cost - (fixedAsset.lifeTime * fixedAsset.depreciationValueYear))}}</td>
            <td class="column-style-fix11">
                <div class="icon-wraper">
                    <div id="addBtn" class="icon-feature icon-feature1" @click="addFixedAssetOnClick"></div>
                    <div class="icon-feature icon-feature2" @click="copyAssetOnClick(fixedAsset)"></div>
                </div>
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
                        <input autocomplete="off" title="Số ban ghi trên một trang" type="text" v-model="defaultPageSize" class="selected-item" @focusin="totalPageSizeOnFocus">
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
            <td class="column-style-fix7">{{formatMoney(this.totalQuantity)}}</td>
            <td class="column-style-fix8" title="Tổng tiền = tổng nguyên giá * tổng số lượng">{{formatMoney(this.totalPrice)}}</td>
            <td class="column-style-fix9">{{formatMoney(this.totalAccumulated)}}</td>
            <td class="column-style-fix10">{{formatMoney(this.totalRemain)}}</td>
            <td class="column-style-fix11"></td>
        </tr>
    </table>
</div>
<!-- Modal -->
<FixedAssetDetailVue v-if="isshowModal" :fixedAssetSelected="fixedAssetSelected" :editMode="editMode" @closeModal="showModal" @showSuccessMessage="successMessage" @resetTable="resetTable" />
<HLoading v-if="isLoading" />
<HToastMessage v-if="isShowSuccess" />
<HDeleteMessage v-if="isShowNotice" :messageDelete="messageDelete" @closeNotice="hideNotice" @deleteFixedAsset="deleteFixedAsset" />
</template>

<style lang="css">
/* Write your own CSS for pagination */
@import url(../../css/base/pagination.css);
</style>

<script>
import axios from "axios";
import FixedAssetDetailVue from "./FixedAssetDetail.vue";
import Paginate from "vuejs-paginate-next";

export default {
    name: "FixedAssetTable",
    components: {
        FixedAssetDetailVue,
        paginate: Paginate
    },

    data() {
        return {
            //Khai báo mảng checkboxArray
            checkboxArray: [],

            //Tính toán các giá trị tổng số
            totalQuantity: 0,
            totalAccumulated: 0,
            totalRemain: 0,
            totalPrice: 0,

            //Đặt giá trị mặc định cho row
            rowStyleOnChecked: false,

            //Đặt thông số mặc định phân trang
            page: 1,
            totalPage: 20,
            recordStart: 1,
            totalRecord: 0,

            //Số lượng bản ghi trên một trang
            defaultPageSize: 10,
            pageSize1: 10,
            pageSize2: 20,
            pageSize3: 30,

            //Đặt giá trị mặc định cho toastMessage
            toastMessage: 0,

            //Đặt mặc định ẩn list
            isShowListDepartments: false,
            isShowListCategoriesAssets: false,
            isShowListPageSize: false,

            //Đặt mặc định ẩn modal
            isshowModal: false,

            //Đặt mặc định ẩn ToastMessage
            isShowSuccess: false,

            //Đặt mặc định ẩn notice
            isShowNotice: false,

            //Đặt mặc định editMode
            editMode: 0,
            insertMode: 0,
            updateMode: 1,
            deleteMode: 2,

            //Đặt mặc định search rỗng
            search: '',

            //Đặt mặc định loading ẩn
            isLoading: false,

            //Đặt mặc định ẩn highline row
            isHighLine: false,
            checked: false,
            isChecked: true,
            checkAll: false,

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //tạo mảng đối tượng employees
            fixedAssets: [],

            //Tạo mảng đối tượng assetCategorys
            fixedAssetCategories: [],

            //tạo đối tượng fixedAssetSelected
            fixedAssetSelected: {},

            //tạo mảng đối tượng phòng ban,
            departments: [],

            //tạo mảng lưu id selected
            idSelected: [],

            //tạo default name
            departmentDefaulName: "Bộ phận sử dụng",
            fixedAssetCategoriesDefaulName: "Loại tài sản",
            nullValue: '',

            //Biến lưu giá trị mã nhân viên mới
            max: 0,
            minLeght: 7,

            //Biến lưu nội dung popup
            messageDelete: '',

            //Tạo biến lưu arrowCounter
            arrowDepartmentsCounter: 0,
            arrowCategoriesCounter: 0,
        }
    },

    methods: {
        /**
         * Format money
         * NDHoang (04/07/2022)
         */
        formatMoney(money) {
            try {
                //Làm tròn dữ liệu đầu vào
                money = Math.floor(money)
                //Ép kiểu thành currency
                var price = new Intl.NumberFormat({
                    style: 'currency',
                    currency: 'VND'
                }).format(money);
                return price;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Reset Table
         * NDHoang (04/07/2022)
         */
        resetTable() {
            try {
                var me = this;
                //Gọi api phân trang
                axios.get(`http://localhost:64168/api/v1/FixedAssets/filter?pageIndex=${me.page}&pageSize=${me.defaultPageSize}&filter=${me.search}&departmentName=${me.nullValue}&CategoryName=${me.nullValue}`)
                    .then(function (res) {
                        //Gán lại giá trị cho đối tượng
                        me.fixedAssets = res.data.data;
                        me.totalPage = Math.ceil(res.data.totalRecord / me.defaultPageSize);
                        me.totalRecord = res.data.totalRecord;
                        me.recordStart = res.data.recordStart;
                        //Ẩn toast message
                        setTimeout(function () {
                            me.isShowSuccess = me.isHide;
                        }, 1500);
                        //Ẩn loading
                        me.isLoading = me.isHide;
                        //Gán lại các giá trị để tính giá trị mới
                        me.totalPrice = 0;
                        me.totalQuantity = 0;
                        me.totalAccumulated = 0;
                        me.totalRemain = 0;
                        //Tính toán lại các giá trị
                        for (const asset of me.fixedAssets) {
                            me.totalPrice += asset.cost;
                            me.totalQuantity += asset.quantity;
                            me.totalAccumulated += (asset.depreciationRate) * asset.lifeTime;
                            me.totalRemain += (asset.cost - ((asset.depreciationRate) * asset.lifeTime));
                            //Tạo mã nhân viên mới
                            var numStr = asset.fixedAssetCode.slice(3, 7);
                            numStr = Number(numStr)
                            if (numStr + 1 > me.max) me.max = numStr + 1;
                        }
                    })
                    .catch(function (res) {
                        console.log(res);
                    })
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện của các phím sử dụng trong combobox category
         * NDHoang(22/06/2022)
         */

        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowListCategoriesDown(cat) {
            try {
                var me = this
                me.isShowListCategoriesAssets = me.isShow;
                if (me.arrowCategoriesCounter < me.fixedAssetCategories.length - 1)
                    me.arrowCategoriesCounter++;
                me.fixedAssetCategoriesDefaulName = cat[me.arrowCategoriesCounter].fixedAssetCategoryName;
            } catch (error) {
                console.log(error);
            }
        },
        // Dùng phím mũi tên lên để di chuyển lựa chọn
        arrowListCategoriesUp(cat) {
            try {
                var me = this
                me.isShowListCategoriesAssets = me.isShow;
                if (me.arrowCategoriesCounter > 0)
                    me.arrowCategoriesCounter--;
                me.fixedAssetCategoriesDefaulName = cat[me.arrowCategoriesCounter].fixedAssetCategoryName;
            } catch (error) {
                console.log();
            }
        },
        //Chọn bằng enter
        selectListCategoriesByEnter() {
            try {
                var me = this;
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện của các phím sử dụng trong combobox category
         * NDHoang(22/06/2022)
         */
        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowDepartmentsDown(dep) {
            try {
                var me = this
                me.isShowListDepartments = me.isShow;
                if (me.arrowDepartmentsCounter < me.fixedAssetCategories.length - 1)
                    me.arrowDepartmentsCounter++;
                me.departmentDefaulName = dep[me.arrowDepartmentsCounter].departmentName;
            } catch (error) {
                console.log(error);
            }
        },
        // Dùng phím mũi tên lên để di chuyển lựa chọn
        arrowDepartmentsUp(dep) {
            try {
                var me = this
                me.isShowListDepartments = me.isShow;
                if (me.arrowDepartmentsCounter > 0)
                    me.arrowDepartmentsCounter--;
                me.departmentDefaulName = dep[me.arrowDepartmentsCounter].departmentName;
            } catch (error) {
                console.log(error);
            }
        },
        //Chọn bằng enter
        selectDepartmentsByEnter() {
            try {
                var me = this;
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện kích nút thêm
         * NDHoang (19/06/2022)
         */
        addFixedAssetOnClick() {
            try {
                var me = this
                //Đổi form mode
                me.editMode = me.insertMode;
                //Hiển thị Modal
                me.showModal(me.isShow);
                //Làm rỗng fixedAssetSelected
                me.fixedAssetSelected = {};

                var newCode = 'TS-' + me.max;
                while (newCode.length < me.minLeght) {
                    newCode = newCode.slice(0, 3) + '0' + newCode.slice(3);
                }
                me.fixedAssetSelected.fixedAssetCode = newCode;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Kiểm tra mảng cần xóa
         * NDHoang (19/06/2022)
         */
        deleteOnClick() {
            try {
                if (this.idSelected.length == 0) {
                    return;
                } else if (this.idSelected.length == 1) {
                    this.messageDelete = `Bạn có muốn xóa tài sản <<${this.fixedAssetSelected.fixedAssetCode}-${this.fixedAssetSelected.fixedAssetName}>>`;
                } else if (this.idSelected.length < 10) {
                    this.messageDelete = '0' + this.idSelected.length + ' tài sản đã được chọn. Bạn có muốn xóa các tài sản này ra khỏi danh sách?';
                } else {
                    this.messageDelete = `${this.idSelected.length}   Tài sản đã được chọn. Bạn có muốn xóa các tài sản này ra khỏi danh sách?`;
                }
                this.hideNotice(this.isShow);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xóa đối tượng
         * NDHoang (19/06/2022)
         */
        deleteFixedAsset() {
            try {
                var me = this;
                //Đổi trạng thái của editMode
                me.editMode = me.deleteMode;
                //Xóa tất cả tài sản trong mảng
                for (let id of me.idSelected) {
                    axios.delete(`http://localhost:64168/api/v1/FixedAssets/${id}`)
                        .then(function () {
                            me.successMessage(me.isShow);
                            me.isLoading = me.isShow;
                            me.resetTable();
                        });
                }
                //Làm rỗng mảng xóa
                this.idSelected = [];
                this.selectedAllRow();
                this.hideNotice(me.isHide);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện onfocus input combobox
         * NDHoang (19/06/2022)
         */
        //Focus on combobox loại tài sản
        fixedAssetCategoriesComboboxOnFocus() {
            try {
                var me = this;
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isShow;
            } catch (error) {
                console.log(error);
            }
        },

        //Focus on combobox Bộ phận sử dụng
        departmentsComboboxOnFocus() {
            try {
                var me = this;
                me.isShowListCategoriesAssets = me.isHide;
                me.isShowListDepartments = me.isShow;
            } catch (error) {
                console.log(error);
            }
        },

        //Focus on combobox PageSize
        totalPageSizeOnFocus() {
            try {
                var me = this;
                me.isShowListPageSize = me.isShow;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện combobox item on click
         * NDHoang (23/06/2022)
         */
        //Click vào combobox bộ phận sử dụng items
        listDepartmentsOnClick(departmentName) {
            try {
                var me = this;
                me.departmentDefaulName = departmentName;
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        //Click vào combobox loại tài sản items
        listFixedAssetCategoriesOnClick(fixedAssetCategoryName) {
            try {
                var me = this;
                me.fixedAssetCategoriesDefaulName = fixedAssetCategoryName;
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        //Click vào combobox pageSize items
        listTotalRecodsOnClick(Size) {
            try {
                var me = this;
                me.defaultPageSize = Size;
                me.isShowListPageSize = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện kích đúp vào dòng
         * NDHoang (19/06/2022)
         */
        rowOnDbClick(fixedAsset) {
            try {
                var me = this;
                me.fixedAssetSelected = fixedAsset;
                me.showModal(me.isShow);
                //Đổi form mode
                me.editMode = me.updateMode;
                //focus vào ô đầu tiên
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện kích đúp vào dòng
         * NDHoang (19/06/2022)
         */
        copyAssetOnClick(fixedAsset) {
            try {
                var me = this;
                me.fixedAssetSelected = fixedAsset;
                var newCode = 'TS-' + me.max;
                while (newCode.length < me.minLeght) {
                    newCode = newCode.slice(0, 3) + '0' + newCode.slice(3);
                }
                me.fixedAssetSelected.fixedAssetCode = newCode;
                me.showModal(me.isShow);
                //Đổi form mode
                me.editMode = me.insertMode;
                //focus vào ô đầu tiên
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gán lại giá trị cho isshowModal
         * NDHoang(22/06/2022)
         */
        showModal(showModal) {
            try {
                var me = this
                me.isshowModal = showModal;
            } catch (error) {
                console.log(error);
            }

        },

        /**
         * Hàm gán ẩn hiện successMessage
         * NDHoang(22/06/2022)
         */
        successMessage(showSuccessMessage) {
            try {
                var me = this;
                me.isShowSuccess = showSuccessMessage;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gán ẩn hiện notice
         * NDHoang(22/06/2022)
         */
        hideNotice(hideNotice) {
            try {
                this.isShowNotice = hideNotice;
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
                if (me.checkboxArray[index] == me.isChecked) {
                    //Bôi đậm dòng được check
                    me.isHighLine = me.isChecked;
                    me.fixedAssetSelected = fixedAsset;
                    me.idSelected.push(fixedAsset.fixedAssetId);
                } else {
                    me.isHighLine = false;
                    //Xóa phần tử không check ra khỏi mảng
                    me.idSelected.splice(index, 1);
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
                //Bôi đậm dòng được checkc
                for (let i = 0; i < me.fixedAssets.length; i++) {
                    me.checkboxArray[i] = me.checkAll;
                    if(me.idSelected.length == 0)
                    me.idSelected.push(me.fixedAssets[i].fixedAssetId);
                    else
                    me.idSelected.pop(me.fixedAssets[i].fixedAssetId);
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gọi api phân trang
         * NDHoang(22/06/2022)
         */
        pagingApi(pageIndex, pageSize, search, departmentName, categoryName) {
            var me = this;
            axios.get(`http://localhost:64168/api/v1/FixedAssets/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${search}&departmentName=${departmentName}&CategoryName=${categoryName}`)
                .then(function (res) {
                    me.fixedAssets = res.data.data;
                    me.totalPage = Math.ceil(res.data.totalRecord / pageSize);
                    me.totalRecord = res.data.totalRecord;
                    me.recordStart = res.data.recordStart;
                    //Gán lại các giá trị để tính giá trị mới
                    me.totalPrice = 0;
                    me.totalQuantity = 0;
                    me.totalAccumulated = 0;
                    me.totalRemain = 0;
                    //Tính toán lại các giá trị
                    for (const asset of me.fixedAssets) {
                        me.totalPrice += asset.cost;
                        me.totalQuantity += asset.quantity;
                        me.totalAccumulated += (asset.depreciationRate * asset.lifeTime);
                        me.totalRemain += asset.cost - (asset.depreciationRate * asset.lifeTime);
                        //Tạo mã nhân viên mới
                        var numStr = asset.fixedAssetCode.slice(3, 7);
                        numStr = Number(numStr)
                        if (numStr + 1 > me.max) me.max = numStr + 1;
                    }
                })
                .catch(function (res) {
                    console.log(res);
                });
        }
    },

    /**
     * Tạo đối tượng tìm kiếm
     * NDHoang(22/06/2022)
     */
    watch: {
        //Khi số trang thay đổi
        page(value) {
            var me = this;
            //Điều kiện phân trang
            if (me.departmentDefaulName == "Bộ phận sử dụng" && me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(value, me.defaultPageSize, me.search, me.nullValue, me.nullValue);
            else if (me.departmentDefaulName == "Bộ phận sử dụng")
                me.pagingApi(value, me.defaultPageSize, me.search, me.nullValue, me.fixedAssetCategoriesDefaulName);
            else if (me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(value, me.defaultPageSize, me.search, me.departmentDefaulName, me.nullValue);
            else
                me.pagingApi(value, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        },

        defaultPageSize(value) {
            var me = this;
            //Điều kiện phân trang
            if (me.departmentDefaulName == "Bộ phận sử dụng" && me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(me.page, value, me.search, me.nullValue, me.nullValue);
            else if (me.departmentDefaulName == "Bộ phận sử dụng")
                me.pagingApi(me.page, value, me.search, me.nullValue, me.fixedAssetCategoriesDefaulName);
            else if (me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(me.page, value, me.search, me.departmentDefaulName, me.nullValue);
            else
                me.pagingApi(me.page, value, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        },

        //Khi ô tìm kiếm thay đổi
        search(value) {
            var me = this;
            //Điều kiện phân trang
            if (me.departmentDefaulName == "Bộ phận sử dụng" && me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(me.page, me.defaultPageSize, value, me.nullValue, me.nullValue);
            else if (me.departmentDefaulName == "Bộ phận sử dụng")
                me.pagingApi(me.page, me.defaultPageSize, value, me.nullValue, me.fixedAssetCategoriesDefaulName);
            else if (me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(me.page, me.defaultPageSize, value, me.departmentDefaulName, me.nullValue);
            else
                me.pagingApi(me.page, me.defaultPageSize, value, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
        },

        //Khi chọn combobox bộ phận sử dụng
        departmentDefaulName(value) {
            var me = this;
            //Điều kiện phân trang
            if (me.fixedAssetCategoriesDefaulName == "Loại tài sản")
                me.pagingApi(me.page, me.defaultPageSize, me.search, value, me.nullValue);
            else
                me.pagingApi(me.page, me.defaultPageSize, me.search, value, me.fixedAssetCategoriesDefaulName);
        },
        //Khi chọn combobox loại tài sản
        fixedAssetCategoriesDefaulName(value) {
            var me = this;
            //Điều kiện phân trang
            if (me.departmentDefaulName == "Bộ phận sử dụng")
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.nullValue, value);
            else
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, value);
        }
    },

    created() {
        //Lấy toàn bộ dữ liệu
        try {
            var me = this;
            me.isLoading = me.isShow;
            me.resetTable();
            //Lấy danh sách phòng ban
            axios.get("http://localhost:64168/api/v1/Departments")
                .then(function (res) {
                    me.departments = res.data;
                })
                .catch(function (res) {
                    console.log(res);
                })
            //Lấy danh sách loại tài sản
            axios.get("http://localhost:64168/api/v1/FixedAssetCategories")
                .then(function (res) {
                    me.fixedAssetCategories = res.data;
                })
                .catch(function (res) {
                    console.log(res);
                })

        } catch (error) {
            console.log(error)
        }

    },

    //3.beforeMount
    beforeMount() {

    },

    //5.beforeUpdate
    beforeUpdate() {

    },

    //6.updated
    updated() {

    },

    //7.beforeUnmount
    beforeUnmount() {

    },

    //8.unmounted
    unmounted() {

    },

}
</script>
