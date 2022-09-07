<template>
<div id="dbEpl" class="modal increase-modal-style" @keyup.esc="closeModalOnclick">
    <div class="modal-layer"></div>
    <div class="modal-body">
        <div class="modal-body-title">
            <div class="modal-body-title_text">{{modalTitle}}</div>
            <div id="closeBtn" class="close-icon" @click="closeModalOnclick"></div>
        </div>
        <div class="body-input-wrap">
            <div class="body-sub-title">Thông tin chứng từ</div>
            <div class="modal-body-input">
                <div class="input-row-wrap">
                    <div class="body-input-row">
                        <div class="body-input_left">
                            <div class="body-input-text">Mã chứng từ
                                <div class="red-tag"></div>
                            </div>
                            <div class="input-icon">
                                <input maxlength="50" ref="txtAssetCodeRef" v-model="license.licenseCode" type="text" class="input modal-input-left" placeholder="Nhập mã tài sản" :class="{'not-valid': inValid.validFixedAssetCode}">
                            </div>
                            <div :class="{'visible': inValid.validFixedAssetCode}" id="txtErrorEmployeeCode" class="input-validate-error">Mã chứng từ không được để trống</div>
                        </div>
                        <div class="body-input_left">
                            <div class="body-input-text">Ngày chứng từ
                                <div class="red-tag"></div>
                            </div>
                            <div class="input-icon">
                                <Datepicker ref="txtProductionDateRef" :maxDate="new Date()" format="dd/MM/yyyy" arrowNavigation locale="vi" placeholder="VD:01/01/2022" cancelText="" selectText="Chọn" v-model="license.useDate" :class="{'not-valid': inValid.validProductionDate}">
                                    <template #input-icon>
                                        <div class="input-icon5"></div>
                                    </template>
                                </Datepicker>
                            </div>
                            <div :class="{'visible': inValid.validProductionDate}" id="txtErrorUsingDate" class="input-validate-error">Ngày bắt đầu sử dụng không được để trống</div>
                        </div>
                        <div class="body-input_left">
                            <div class="body-input-text">Ngày ghi tăng
                                <div class="red-tag"></div>
                            </div>
                            <div class="input-icon">
                                <Datepicker ref="txtPurchaseDateRef" :maxDate="new Date()" format="dd/MM/yyyy" arrowNavigation locale="vi" placeholder="VD:01/01/2022" cancelText="" selectText="Chọn" v-model="license.writeUpdate" :class="{'not-valid': inValid.validPurchaseDate}">
                                    <template #input-icon>
                                        <div class="input-icon5"></div>
                                    </template>
                                </Datepicker>
                            </div>
                            <div :class="{'visible': inValid.validPurchaseDate}" id="txtErrorByingDate" class="input-validate-error">Ngày mua không được để trống</div>
                        </div>
                    </div>
                    <div class="body-input-row input-increase-style">
                        <div class="validate-text1">
                            <div class="body-input-text">Ghi chú
                            </div>
                            <div class="input-icon">
                                <input maxlength="500" type="text" class="input modal-input-full" v-model="license.description">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="body-sub-title">Thông tin chi tiết</div>
            <div class="input-row-wrap horizontal-style">
                <div class="content-menu-input">
                    <div class="input-icon">
                        <div class="input-icon1"></div>
                        <input maxlength="255" @keyup.enter="selectListByEnter" id="txtSearch" v-model="search" type="text" class="input search-input" placeholder="Tìm kiếm theo mã, tên tài sản">
                    </div>
                </div>
                <button class="main-button white-rectangle-button modal-button-left" @click="showChosen">Chọn tài sản</button>
            </div>
            <table class="content-table-title">
                <tr class="table-column-title">
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
                <tr v-for="(fixedAsset,index) in assets" :key="fixedAsset.fixedAssetId" class="table-column-infor">
                    <!-- v-model="checked" -->
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
                        <div class="icon-wraper">
                            <div title="Sửa tài sản" id="addBtn" class="icon-feature icon-feature1" @click.stop="showSelected(fixedAsset,index)"></div>
                            <div title="Xóa tài sản" class="icon-feature icon-feature2" @click.stop="deleteOnClick(index)"></div>
                        </div>
                    </td>
                </tr>
            </table>
            <table id="tblFooterPrice" class="content-table-title footer-table">
                <tr class="table-column-title increase-footer">
                    <td class="column-increse2"></td>
                    <td class="column-increse3"></td>
                    <td class="column-increse4"></td>
                    <td class="column-increse5"></td>
                    <td class="column-increse6">{{formatMoney(this.totalPrice)}}</td>
                    <td class="column-increse7">{{formatMoney(this.totalAccumulated)}}</td>
                    <td class="column-increse8">{{formatMoney(this.totalPrice-this.totalAccumulated)}}</td>
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
        </div>
        <div class="modal-footer">
            <button id="btnSave" class="main-button rectangle-button modal-button-right" @click="saveOnClick">Lưu</button>
            <button id="btnCancle" class="main-button white-rectangle-button modal-button-left" @click="cancelOnClick">Hủy</button>
        </div>
    </div>
</div>
<HErrorMessage v-if="isShowErrorNotice" @closeNotice="showErrorNotice" :errorMessage="errorMessage" />
<HPostMessage v-if="isShowPostNotice" @closeNotice="showPostNotice" @closeModal="closeModal" />
<HPutMessage v-if="isShowPutNotice" @closeNotice="showPutNotice" @closeModal="closeModal" @saveOnClick="saveOnClick" />
</template>

<style lang="css">
/* Write your own CSS for pagination */
/* Css của paginate */
@import url(../../css/base/pagination.css);

@import url(../../css/base/datepicker.css);

@import url(../../css/layout/increasetable.css);
</style>

<script>
import resources from '../../js/resource.js';
import enums from '../../js/enum.js';
//sử dụng axioss
import axios from 'axios';
//Sử dụng thư viện paginate
import Paginate from "vuejs-paginate-next";

import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

export default {
    name: "EmployeeDetail",
    emits: ["closeModal", "resetTable", "showSuccessMessage","closeChosen","closeSelected","getLicenseDetailId","getFixedAssetSelected","getListAsset","getFixedAsset"],
    components: {
        Datepicker,
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
            license: {},

            //Tạo mảng hứng tài sản từ chosen
            selectedDetailFixedAssets: [],

            //Tạo mảng chứa detailIds của license
            licenseDetailIds: [],

            //Đặt mặc định ẩn cảnh báo
            isShowPostNotice: false,
            isShowPutNotice: false,
            isShowErrorNotice: false,

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //Tạo biến chứa error message
            errorMessage: [],

            //Tạo biến search
            search: '',

            //Đặt giá trị mặc định cho mỗi trường validate
            inValid: {
                validFixedAssetCode: false,
            },

            //Tạo biến check valid
            isValid: true,

            //Mặc định ẩn lish
            isShowListPageSize: false,
        }
    },

    /**
     * Khai báo prop sử dụng bên ngoài
     */
    props: {
        selectedFixedAssets: {
            type: Array
        },

        jsonDetailSelected: {
            type: Array
        },

        licenseSelected: {
            type: Object
        },
        editMode: {
            type: Number,
            default: 0, // thêm mới
            require: true
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
                money = String(Math.floor(money))
                return money.replace(/\B(?=(\d{3})+(?!\d))/g, resources.currency);
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
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.licenseSelected.licenseId);
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
        
        //Hiển thị modal chosen asset
        showChosen(){
            this.$emit("closeChosen", this.isShow);
        },

        //Hiển thị cảnh báo trước khi tắt
        closeModalOnclick() {
            try {
                var me = this
                //Xét trạng thái modal
                if (me.editMode == enums.editMode.insertMode) {
                    //Hiện thông báo cho thêm mới
                    me.showPostNotice(me.isShow);
                } else {
                    //Hiện thông báo cho sửa
                    me.showPutNotice(me.isShow);
                }
            } catch (error) {
                console.log(error);
            }

        },

        //Hiển thị cảnh báo trước khi tắt
        cancelOnClick() {
            try {
                var me = this
                //Xét trạng thái modal
                if (me.editMode == enums.editMode.insertMode) {
                    //Hiện thông báo cho thêm mới
                    me.showPostNotice(me.isShow);
                } else {
                    //Hiện thông báo cho sửa
                    me.showPutNotice(me.isShow);
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Chọn bằng Enter
         * NDHoang(22/07/2022)
         */
        selectListByEnter() {
            try {
                var me = this;
                //Ẩn danh sách
                me.pagingApi(me.page, me.defaultPageSize, me.search , me.licenseSelected.licenseId);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Tab ra ngoài combobox
         * NDHoang(22/07/2022)
         */
        tabOutCombobox() {
            try {
                var me = this;
                //Ẩn danh sách
                me.isShowListPageSize = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xóa tài sản trong hàng
         * NDHoang(22/06/2022)
         */
        deleteOnClick(index){
            var me = this;
            //reset và tính toán lại các giá trị
            me.totalPrice = 0;
            me.totalAccumulated=0;
            me.totalRemain= 0;
            if(me.selectedFixedAssets.length != 0){
                me.selectedFixedAssets.splice(index, 1);
            }
            else {
                me.assets.splice(index, 1);
                me.$emit("getFixedAsset",me.assets);
            }
            for (const selectedFixedAsset of me.selectedFixedAssets) {
                me.totalPrice += selectedFixedAsset.cost;
                me.totalAccumulated += selectedFixedAsset.depreciationValueYear;
                me.totalRemain += selectedFixedAsset.cost - selectedFixedAsset.depreciationValueYear;
            }
            me.pagingApi(me.page, me.defaultPageSize, me.search , me.licenseSelected.licenseId);
        },

        /**
         * Show modal selected
         * NDHoang(22/06/2022)
         */
        showSelected(fixedAsset,index){
            this.$emit("closeSelected",this.isShow);
            if(this.licenseDetailIds.length == 0){
                this.$emit("getFixedAssetSelected",fixedAsset,index)
                this.$emit("getLicenseDetailId",'',index);
            }else{
                this.$emit("getLicenseDetailId",this.licenseDetailIds[index].licenseDetailId,index);
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
        async saveOnClick() {
            var me = this;
            try {
                //Tạo đối tượng mới
                const LicenseDetails = [];
                var Details = {};
                for (var i = 0; i < me.assets.length; i++) {
                    Details = {
                        fixedAssetId: me.assets[i].fixedAssetId,
                        detailJson: me.jsonDetailSelected[i],
                    }
                    LicenseDetails.push(Details);
                }

                const newlicense = {
                    licenseId: me.license.licenseId,
                    licenseCode: me.license.licenseCode,    
                    useDate: me.license.useDate,
                    writeUpdate: me.license.writeUpdate,
                    description: me.license.description,
                    total: me.totalPrice,
                    licenseDetails: LicenseDetails
                }
                //Validate dữ liệu
                if (!me.validate()) return;
                else {
                    if (me.editMode == enums.editMode.insertMode) {
                        try {
                            //Post đối tượng
                            axios.post(resources.url.host + "/api/v1/LicenseAdds/multiData", newlicense)
                                .catch(function (error) {
                                    if (error.response) {
                                        //Hiển thị popup lỗi
                                        me.errorMessage = [];
                                        for (const errorMes of error.response.data.data.Error) {
                                            me.errorMessage.push(errorMes);
                                        }
                                        console.log(error.response.data.data);
                                        console.log(error.response.status);
                                        me.showErrorNotice(me.isShow);
                                    }
                                })
                                .then(function (res) {
                                    if (res) {
                                        
                                        //Tắt modal
                                        me.$emit("closeModal", me.isHide);
                                        me.$emit("showSuccessMessage", me.isShow);
                                        //Hiển thị thông báo thành công
                                        me.$emit("resetTable");
                                    }
                                })
                        } catch (error) {
                            console.log(error);
                        }
                    }
                    //Sửa đối tượng
                    else {
                        //Put đối tượng
                        try {
                            //Xóa đối tượng được chọn
                            axios.put(resources.url.host + `/api/v1/LicenseAdds/${me.license.licenseId}`, newlicense)
                                .catch(function (error) {
                                    if (error.response) {
                                        //Hiển thị popup lỗi
                                        me.errorMessage = [];
                                        for (const errorMes of error.response.data.data.Error) {
                                            me.errorMessage.push(errorMes);
                                        }
                                        console.log(error.response.data.data);
                                        console.log(error.response.status);
                                        me.showErrorNotice(me.isShow);
                                    }
                                })
                                .then(function (res) {
                                    if (res) {
                                        //Tắt modal
                                        me.$emit("closeModal", me.isHide);
                                        me.$emit("showSuccessMessage", me.isShow);
                                        //Hiển thị thông báo thành công
                                        me.$emit("resetTable");
                                    }
                                })
                        } catch (error) {
                            console.log(error);
                        }
                    }
                }
            } catch (error) {
                console.log(error)
            }
        },

        validate() {
            try {
                var me = this;
                me.isValid = true;
                //Check ngày sử dụng
                if (!me.license.useDate) {
                    me.isValid = false;
                    me.inValid.validProductionDate = true;
                    me.$refs.txtProductionDateRef.focus();
                } else {
                    me.inValid.validProductionDate = false;
                }
                //Check ngày mua
                if (!me.license.writeUpdate) {
                    me.isValid = false;
                    me.inValid.validPurchaseDate = true;
                    me.$refs.txtPurchaseDateRef.focus();
                } else {
                    me.inValid.validPurchaseDate = false;
                }

                //Check mã tài sản
                if (!me.license.licenseCode) {
                    me.isValid = false;
                    me.inValid.validFixedAssetCode = true;
                    me.$refs.txtAssetCodeRef.focus();
                } else {
                    me.inValid.validFixedAssetCode = false;
                }
                if (!me.isValid) {
                    return false;
                } else return true;

            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gọi api phân trang
         * NDHoang(22/06/2022)
         */
        pagingApi(pageIndex, pageSize, search, licenseId) {
            var me = this;
            //Gọi api phân trang
            axios.get(resources.url.host + `/api/v1/LicenseDetails/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${search}&licenseId=${licenseId}`)
                .then(function (res) {
                    if(me.selectedDetailFixedAssets.length == 0){
                        me.assets = res.data.data;
                        me.licenseDetailIds = res.data.id;
                        me.totalPage = Math.ceil(res.data.totalRecord / pageSize);
                        me.totalRecord = res.data.totalRecord;
                        me.recordStart = res.data.recordStart;
                    } else {
                        me.assets = me.selectedDetailFixedAssets
                        me.totalPage = 1;
                        me.totalRecord = me.selectedDetailFixedAssets.length;
                    }
                    //Truyền mảng về trang chính
                    me.$emit("getListAsset",me.assets);
                    //Gán lại các giá trị để tính giá trị mới
                    me.totalPrice = 0;
                    me.totalAccumulated = 0;
                    me.totalRemain = 0;
                    //Tính toán lại các giá trị
                    for (const asset of me.assets) {
                        me.totalPrice += asset.cost;
                        me.totalAccumulated += asset.depreciationValueYear;
                        me.totalRemain += asset.cost - asset.depreciationValueYear;
                    }
                })
                .catch(function (res) {
                    console.log(res);
                });
        },

        /**
         * Xử lý dữ convert sang date
         * NDHoang(05/07/2022)
         */
        convertDate(date) {
            return new Date(date);
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
            me.pagingApi(value, me.defaultPageSize, me.search, me.licenseSelected.licenseId);
        },

        //Khi pageSize thay đổi
        defaultPageSize(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, value, me.search, me.licenseSelected.licenseId);
        },

        //Khi ô tìm kiếm thay đổi
        search(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, me.defaultPageSize, value, me.licenseSelected.licenseId);
        },

        //Khi chọn lại tài sản
        selectedFixedAssets(value) {
            var me = this;
            me.selectedDetailFixedAssets = value;
            me.pagingApi(me.page, me.defaultPageSize, me.search, me.licenseSelected.licenseId);
        }
    },

    created() {
        var me = this;
        //Load lại table
        me.pagingApi(me.page, me.defaultPageSize, me.search, me.licenseSelected.licenseId);
        //gán giá trị chứng từ được chọn cho cho fixedAsset
        me.license = me.licenseSelected;
        //Set lại múi giờ cho chứng từ
        me.license.useDate = me.convertDate(me.convertDate(me.license.useDate ).setTime(me.convertDate(me.license.useDate ).getTime() + 7*3600000));
        me.license.writeUpdate = me.convertDate(me.convertDate(me.license.writeUpdate ).setTime(me.convertDate(me.license.writeUpdate ).getTime() + 7*3600000));
        //Xét trạng thái modal tương ứng
        if (me.editMode == enums.editMode.insertMode) {
            me.modalTitle = resources.modalLicenseTitle.insertTitle;
            //Đặt giá trị mặc định cho ngày mua và ngày bắt đầu sử dụng
            me.license.writeUpdate = new Date();
            me.license.useDate = new Date();
        } else me.modalTitle = resources.modalLicenseTitle.updateTitle;
    },

    mounted() {
        var me = this;
        //Focus vào ô đầu tiên
        me.$refs.txtAssetCodeRef.focus();
    }
}
</script>
