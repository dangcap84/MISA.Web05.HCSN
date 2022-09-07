<template>
<div class="content-menu">
    <div class="content-menu-title">
        Ghi tăng tài sản
    </div>
    <div class="content-menu-button">
        <button id="btnAdd" class="main-button rectangle-button" @click="addFixedAssetOnClick">
            Thêm
        </button>
        <div class="display-button">
            <div class="display-state" title="Trạng thái hiển thị">
                <div class="display-state-icon" :class="{'display-state-icon-sub':isShowDetail}"></div>
            </div>
            <div class="display-arrow" @click="showDetailTable" title="Thay đổi trạng thái hiển thị">
                <div class="display-arrow-icon" :class="{'display-arrow-icon-sub':isShowDetail}"></div>
            </div>
        </div>
    </div>
</div>
<div class="content-menu-wrap">
    <div class="content-menu" :class="{'display-none' :isShowFullDetail}">
        <div class="content-menu-input">
            <div class="input-icon">
                <div class="input-icon1"></div>
                <input maxlength="255" @keyup.enter="selectListByEnter" id="txtSearch" v-model="search" type="text" class="input search-input" placeholder="Tìm kiếm theo số chứng từ, nội dung">
            </div>
        </div>
        <div class="content-menu-button">
            <div class="display-button">
                <div class="display-state" title="Xóa nhiều" v-if="isShowMultiDelete" @click.stop="deleteOnClick" style="margin-right: 10px;">
                    <div class="delete-icon"></div>
                </div>
                <div class="display-state" title="In">
                    <div class="print-icon"></div>
                </div>
                <div class="display-state" title="Tùy chọn" style=" width: 2px; padding-left: 18px;">
                    <div class="option-icon"></div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="content-table-wrap wrap-increase-style" :class="{'fix-table':isShowDetail, 'display-none' :isShowFullDetail}">
    <table class="content-table-title">
        <tr class="table-column-title">
            <td class="column-style-fix1">
                <input class="input-checkbox" type="checkbox" v-model="checkAll" @change="selectedAllRow()">
            </td>
            <td class="column-style-fix2" title="Số thứ tự">STT</td>
            <td class="column-style-fix3" title="Số chứng từ">Số chứng từ</td>
            <td class="column-style-fix4" title="Ngày chứng từ<">Ngày chứng từ</td>
            <td class="column-style-fix5" title="Ngày ghi tăng">Ngày ghi tăng</td>
            <td class="column-style-fix8" title="Tổng nguyên giá">Tổng nguyên giá</td>
            <td class="column-style-fix9" title="Nội dung">Ghi chú</td>
            <td class="column-style-fix11"></td>
        </tr>
    </table>
    <table id="tblListEmployees" class="content-table">
        <tr v-for="(license,index) in licenses" :key="license.licenseId" class="table-column-infor" @contextmenu="onContextMenu($event,license,index)" @dblclick="rowOnDbClick(license)" :class="{'row-selected': checkboxArray[index]}" @click="clickOnRow(license, index)">
            <!-- v-model="checked" -->
            <td class="column-style-fix1"><input class="input-checkbox" v-model="checkboxArray[index]" type="checkbox"></td>
            <td class="column-style-fix2">{{recordStart+index}}</td>
            <td class="column-style-fix3">{{license.licenseCode}}</td>
            <td class="column-style-fix4">
                <div :title="license.useDate" class="tabletext-wrap">
                    {{formatDate(license.useDate)}}
                </div>
            </td>
            <td class="column-style-fix5">
                <div :title="license.writeUpdate" class="tabletext-wrap">
                    {{formatDate(license.writeUpdate)}}
                </div>
            </td>
            <td class="column-style-fix8">{{formatMoney(license.total)}}</td>
            <td class="column-style-fix9">
                <div :title="license.description" class="tabletext-wrap">
                    {{license.description}}
                </div>
            </td>
            <td class="column-style-fix11">
                <div class="icon-wraper">
                    <div title="Sửa chứng từ" id="addBtn" class="icon-feature icon-feature1" @click.stop="rowOnDbClick(license)"></div>
                    <div title="Xóa chứng từ" class="icon-feature icon-feature2" @click.stop="deleteOnClick"></div>
                </div>
            </td>
        </tr>
    </table>
    <table id="tblFooterPrice" class="content-table-title footer-table">
        <tr class="table-column-title increase-footer">
            <td class="column-style-fix1">
            </td>
            <td class="column-style-fix2"></td>
            <td class="column-style-fix3"></td>
            <td class="column-style-fix4"></td>
            <td class="column-style-fix5"></td>
            <td class="column-style-fix8">{{formatMoney(totalPrice)}}</td>
            <td class="column-style-fix11"></td>
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
<div v-if="isShowDetail" class="content-table-wrap wrap-increase-style wrap-subtable-style" :class="{'full-table' :isShowFullDetail}">
    <table id="tblFooterPrice" class="content-table-title footer-table footer-table-increase">
        <tr class="table-column-title table-footer">
            <td class="column-increse10">Thông tin chi tiết</td>
            <td class="column-increse9" :class="{'display-full-icon-sub':isShowFullDetail}" @click="showFullDetailTable"></td>
        </tr>
    </table>
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
        <tr v-for="(fixedAssetLicense,index) in fixedAssetLicenses" :key="fixedAssetLicense.licenseId" class="table-column-infor">
            <!-- v-model="checked" -->
            <td class="column-increse2">{{recordStart+index}}</td>
            <td class="column-increse3">{{fixedAssetLicense.FixedAssetCode}}</td>
            <td class="column-increse4">
                <div :title="fixedAssetLicense.FixedAssetName" class="tabletext-wrap">
                    {{fixedAssetLicense.FixedAssetName}}
                </div>
            </td>
            <td class="column-increse5">
                <div :title="fixedAssetLicense.DepartmentName" class="tabletext-wrap">
                    {{fixedAssetLicense.DepartmentName}}
                </div>
            </td>
            <td class="column-increse6">{{formatMoney(fixedAssetLicense.Cost)}}</td>
            <td class="column-increse7">{{formatMoney(fixedAssetLicense.DepreciationValueYear)}}</td>
            <td class="column-increse8">
                {{formatMoney(fixedAssetLicense.Cost-fixedAssetLicense.DepreciationValueYear)}}
            </td>
        </tr>
    </table>
</div>
<IncreaseWritingDetailVue v-if="isShowModal" :licenseSelected="licenseSelected" :selectedFixedAssets="selectedFixedAssets" :jsonDetailSelected="jsonDetailSelected" @getListAsset="getListAsset" @getFixedAsset="getFixedAsset" @getFixedAssetSelected="getFixedAssetSelected" @getLicenseDetailId="getLicenseDetailId" :editMode="editMode" @closeModal="showModal" @closeChosen="showChosen" @closeSelected="showSelected" @showSuccessMessage="successMessage" @resetTable="resetTable" />
<IncreaseChosenAssetVue v-if="isShowChosen" :editMode="editMode" :fixedAssetLicenses="fixedAssetLicenses" :listAsset="listAsset" @closeChosen="showChosen" @getFixedAsset="getFixedAsset" />
<IncreaseSelectedDetailVue v-if="isShowSelectedDetail" :licenseSelected="licenseSelected" :licenseDetailIdSelected="licenseDetailIdSelected" :jsonDetailSelected="jsonDetailSelected" :fixedAssetSelected="fixedAssetSelected" :indexRowDetail="indexRowDetail" @getJsonDetail="getJsonDetail" @closeSelected="showSelected" @showSuccessMessage="successMessage" />
<HLoading v-if="isLoading" />
<HNoticeMessage v-if="isShowNoticeMessage" @closeNotice="showNoticeMessage" @showDetail="showDetailMessage" :noticeMessage="noticeMessage" />
<HDetailMessage v-if="isShowDetailMessage" @closeNotice="showDetailMessage" :detailMessage="detailMessage" />
<HToastMessage v-if="isShowSuccess" />
<HDeleteMessage v-if="isShowNotice" :messageDelete="messageDelete" @closeNotice="hideNotice" @deleteFixedAsset="deleteFixedAsset" />
</template>

<style lang="css">
/* Css của paginate */
@import url(../../css/base/pagination.css);
/* Css của context menu */
@import url(../../css/base/contextmenu.css);

@import url(../../css/layout/increasetable.css);
</style>

<script>
import resources from '../../js/resource.js'
import enums from '../../js/enum.js';
//Sử dụng axioss
import axios from "axios";
//Sử dụng components view FixedAssetDetail
import IncreaseWritingDetailVue from "./IncreaseWritingDetail.vue";
import IncreaseChosenAssetVue from "./IncreaseChosenAsset.vue";
import IncreaseSelectedDetailVue from "./IncreaseSelectedDetail.vue";
//Sử dụng thư viện paginate
import Paginate from "vuejs-paginate-next";

export default {
    name: "IncreaseWritingTable",
    //Tạo components
    components: {
        IncreaseWritingDetailVue,
        IncreaseChosenAssetVue,
        IncreaseSelectedDetailVue,
        paginate: Paginate,
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

            //Đặt mặc định ẩn list
            isShowListPageSize: false,

            //Mặc định ẩn thông báo
            isShowNoticeMessage: false,
            isShowDetailMessage: false,

            //Đặt mặc định ẩn modal
            isShowModal: false,
            isShowChosen: false,
            isShowSelectedDetail: false,

            //Đặt mặc định ẩn thanh detail
            isShowDetail: true,
            //Đặt mặc định ẩn full size
            isShowFullDetail: false,

            //Đặt mặc định ẩn ToastMessage
            isShowSuccess: false,

            //Đặt mặc định ẩn notice
            isShowNotice: false,

            //Đặt mặc định ẩn multiDelete
            isShowMultiDelete: false,

            //Đặt mặc định editMode
            editMode: 0,
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

            //tạo mảng lưu id selected
            idSelected: [],

            //Tạo mảng lưu detailIds
            details: [],
            //Tạo đối tượng lưu fixedAsset
            fixedAssetSelected:{},
            listAsset: [],
            //Tạo đối tượng lưu detailId
            licenseDetailIdSelected: '',
            indexRowDetail: 0,

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //tạo mảng đối tượng chứng từ
            licenses: [],
            //Tạo mảng tài sản thuộc chứng từ theo LicenseId
            fixedAssetLicenses: [],

            //Tạo mảng hứng tài sản được chọn để binding vào form
            selectedFixedAssets: [],

            //Tạo mảng hứng detail json
            jsonDetailSelected: [],

            //tạo đối tượng licenseSelected
            licenseSelected: {},

            //Tạo biến lưu mã tài sản mới
            newCode: '',

            //Biến lưu nội dung popup
            messageDelete: '',

            //Biến lưu nội dung thông báo
            noticeMessage: [],
            detailMessage: [],

            //Tạo biến lưu arrowCounter
            arrowDepartmentsCounter: 0,
            arrowCategoriesCounter: 0,

            //Tạo biến lưu file
            file: '',

            //Tạo biến lưu count
            count: 0,
        }
    },

    methods: {
        /**
         * Xử lý ngày hiển thị
         * NDHoang (04/07/2022)
         */
        formatDate(dob) {
            let dateConvert = new Date(dob);
            if (dob && (dateConvert instanceof Date && !isNaN(dateConvert.valueOf()))) {
                // xử lý ngày
                let date = dateConvert.getDate();
                date = date < 10 ? `0${date}` : date;
                // xử lý tháng
                let month = dateConvert.getMonth() + 1;
                month = month < 10 ? `0${month}` : month;
                // xử lý năm
                let year = dateConvert.getFullYear();
                dob = `${date}/${month}/${year}`;
                return dob;
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
         * Chức năng context menu cho từng tài sản
         * NDHoang (19/07/2022)
         */
        onContextMenu(e, license, index) {
            e.preventDefault();
            //Hiện menu
            this.$contextmenu({
                x: e.x,
                y: e.y,
                items: [{
                        label: "Thêm",
                        onClick: () => {
                            this.addFixedAssetOnClick();
                        },
                    },
                    {
                        label: "Sửa",
                        onClick: () => {
                            this.rowOnDbClick(license);
                        },
                    },
                    {
                        label: "Xóa",
                        onClick: () => {
                            this.deleteFixedByContextMenu(license,index);
                        },
                    },
                ]
            });
        },

        /**
         * Hứng lấy tài sản được chọn
         * NDHoang (04/07/2022)
         */
        getFixedAsset(assetsSelected) {
            this.selectedFixedAssets = assetsSelected;
            this.listAsset = assetsSelected;
        },
        /**
         * Hứng lấy tài sản được chọn
         * NDHoang (04/07/2022)
         */
        getJsonDetail(jsonDetail, index) {
            this.jsonDetailSelected[index] = jsonDetail
        },

        /**
         * Reset Table
         * NDHoang (04/07/2022)
         */
        resetTable() {
            try {
                var me = this;
                //Gọi api phân trang
                axios.get(resources.url.host + `/api/v1/Licenses/filter?pageIndex=${me.page}&pageSize=${me.defaultPageSize}&filter=${me.search}`)
                    .then(function (res) {
                        //Gán lại giá trị cho đối tượng
                        me.licenses = res.data.data;
                        me.totalPage = Math.ceil(res.data.totalRecord / me.defaultPageSize);
                        me.totalRecord = res.data.totalRecord;
                        me.recordStart = res.data.recordStart;
                        //Tính tổng giá trị
                        me.totalPrice = 0;
                        for (var license of me.licenses) {
                            me.totalPrice += license.total;
                        }
                        //Ẩn toast message
                        setTimeout(function () {
                            me.isShowSuccess = me.isHide;
                        }, 2000);
                        //Ẩn loading
                        me.isLoading = me.isHide;
                        me.getNewCode();
                    })
                    .catch(function (res) {
                        console.log(res);
                    })
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Lấy mã tài sản mới
         * NDHoang (04/07/2022)
         */
        getNewCode() {
            var me = this;
            axios.get(resources.url.host + "/api/v1/Licenses/NewLicenseCode")
                .then(function (res) {
                    me.newCode = res.data;
                });
        },

        /**
         * Sự kiện kích nút thêm
         * NDHoang (19/06/2022)
         */
        addFixedAssetOnClick() {
            try {
                var me = this
                //Đổi form mode
                me.editMode = enums.editMode.insertMode;
                //Hiển thị Modal
                me.showModal(me.isShow);
                //Làm rỗng licenseSelected
                me.licenseSelected = {};
                //Format lại mã tài sản rồi gán lại
                me.licenseSelected.licenseCode = me.newCode;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hiện thông báo
         * NDHoang(22/06/2022)
         */
        //Cảnh báo lỗi
        showNoticeMessage(noticeMessage) {
            try {
                var me = this;
                //Hiện thông báo nếu errorNotice = true
                me.isShowNoticeMessage = noticeMessage;
            } catch (error) {
                console.log(error);
            }
        },
        //Hiện thông báo chi tiết
        showDetailMessage(detailMessage) {
            try {
                var me = this;
                //Hiện thông báo nếu detailMessage = true
                me.isShowDetailMessage = detailMessage;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Chọn file import
         * NDHoang (19/06/2022)
         */
        importOnClick() {
            try {
                var me = this;
                //Làm rỗng value của input file
                me.$refs.fileImport.value = "";
                //Click import hiển thị cửa số chọn file
                me.$refs.fileImport.click();
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
                //Điều kiện xóa
                this.count = 0;
                for (const id of this.idSelected) {
                    if (id) {
                        this.count++;
                    }
                }
                if (this.count == 0) {
                    return;
                } else if (this.count == 1) {
                    this.messageDelete = resources.deleteLicense.beginText + `${this.licenseSelected.licenseCode}` + resources.deleteLicense.endText;
                } else if (this.count < 10) {
                    this.messageDelete = resources.import.isZero + this.count + resources.deleteLicense.mutilDelete;
                } else {
                    this.messageDelete = this.count + resources.deleteLicense.mutilDelete;
                }
                //Ẩn thông báo
                this.hideNotice(this.isShow);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xóa đối tượng
         * NDHoang (19/06/2022)
         */
        async deleteFixedAsset() {
            try {
                var me = this;
                //Đổi trạng thái của editMode
                me.editMode = enums.editMode.deleteMode;
                //Xóa chi tiết chứng từ
                //Chuyển mảng id thành string
                var detailIds = me.details.join(',');
                var listIdSelected = me.idSelected.join(',');
                if (detailIds) {
                    await axios.delete(resources.url.host + `/api/v1/LicenseDetails/filterId?licenseDetailIds=${detailIds}`)
                }

                //Xóa chứng từ trong mảng
                axios.delete(resources.url.host + `/api/v1/Licenses/filterId?licenseIds=${listIdSelected}`)
                    .then(function () {
                        me.successMessage(me.isShow);
                        me.isLoading = me.isShow;
                        me.resetTable();
                    });
                //Làm rỗng mảng xóa
                me.idSelected = [];
                me.detailIds = [];
                me.fixedAssetLicenses = [];
                //Ẩn xóa nhiều
                me.isShowMultiDelete = me.isHide;
                //Bỏ chọn tất cả row
                me.checkAll = false;
                me.selectedAllRow();
                //Ẩn thông báo
                me.hideNotice(me.isHide);
            } catch (error) {
                console.log(error);
            }
        },

        //Xóa bằng context menu
        deleteFixedByContextMenu(license,index) {
            try {
                var me = this;
                //Gán lại mảng rỗng
                me.idSelected = [];
                //Đổi trạng thái của editMode
                me.editMode = enums.editMode.deleteMode;
                //Gán đối tượng selected
                me.licenseSelected = license;
                //Thêm id vào mảng
                me.idSelected[index] = license.licenseId;
                //Gọi hàm delete
                me.deleteOnClick();

            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện kích đúp vào dòng
         * NDHoang (19/06/2022)
         */
        async rowOnDbClick(license) {
            try {
                var me = this;
                //Gán lại đối tượng selected
                me.licenseSelected = license;
                //Hiện modal
                me.showModal(me.isShow);
                //Đổi form mode
                me.editMode = enums.editMode.updateMode;
                //focus vào ô đầu tiên
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm chọn check box khi click vào dòng
         * NDHoang(22/06/2022)
         */
        clickOnRow(license, index) {
            try {
                var me = this;
                if (me.checkboxArray[index] == me.isChecked) {
                    me.checkboxArray[index] = false;
                } else {
                    me.checkboxArray[index] = me.isChecked
                }
                me.selectedRow(license, index);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm chon dòng
         * NDHoang(22/06/2022)
         */
        async selectedRow(license, index) {
            try {
                var me = this;
                var i = 0;
                //Điều kiện
                if (me.checkboxArray[index] == me.isChecked) {
                    //Hiện nút xóa nhiều
                    var counter = 0;
                    for (i = 0; i < me.checkboxArray.length; i++) {
                        if (me.checkboxArray[i]) {
                            counter++;
                        }
                    }
                    if (counter > 1) me.isShowMultiDelete = me.isShow;
                    //Bôi đậm dòng được check
                    me.isHighLine = me.isChecked;
                    //Gán lại giá trị selected
                    me.licenseSelected = license;
                    //Thêm id vào mảng
                    me.idSelected[index] = license.licenseId;
                    //Lấy danh sách tài sản thuộc chứng từ theo id
                    var paramAxios = {
                        licenseId: license.licenseId
                    }
                    await axios.get(resources.url.host + "/api/v1/LicenseDetails/getDetailAssets", {
                            params: paramAxios
                        })
                        .then(function (res) {
                            me.fixedAssetLicenses = res.data;
                        }).catch(function (err) {
                            console.log(err);
                        })
                    //Thêm detailIds chạy hết độ dài của detail
                    for (i = 0; i < me.fixedAssetLicenses.length; i++) {
                        //Khi gặp phần tử cuối cùng tự động ghi tiếp
                        while (me.details[index + i]) {
                            index++;
                        }
                        me.details[index + i] = me.fixedAssetLicenses[i].LicenseDetailId;
                    }
                    //Hiện check all
                    counter = 0;
                    for(i =0 ;i < me.idSelected.length; i++){
                        if(me.idSelected[i]){
                            counter++;
                        }
                    }
                    if(counter == me.licenses.length) me.checkAll = me.isShow;
                } else {
                    //Ẩn nút xóa nhiều
                    counter = 0;
                    for (i = 0; i < me.checkboxArray.length; i++) {
                        if (me.checkboxArray[i]) {
                            counter++;
                        }
                    }
                    if (counter <= 1) me.isShowMultiDelete = me.isHide;

                    //Xóa bôi đậm dòng
                    me.isHighLine = false;

                    //Kiểm tra nếu còn một tài sản duy nhất được check thì hiển thị lên màn hình
                    for (i = 0; i < me.checkboxArray.length; i++) {
                        if (me.checkboxArray[i]) {
                            me.licenseSelected = me.licenses[i];
                        }
                    }
                    //Xóa phần tử không check ra khỏi mảng
                    me.idSelected[index] = '';
                    //Lấy danh sách tài sản thuộc chứng từ
                    //Tạo tham số truyền vào axios
                    paramAxios = {
                        licenseId: license.licenseId
                    }
                    //Gọi api lấy mảng tài sản detail
                    await axios.get(resources.url.host + "/api/v1/LicenseDetails/getDetailAssets", {
                            params: paramAxios
                        })
                        .then(function (res) {
                            me.fixedAssetLicenses = res.data;
                        }).catch(function (err) {
                            console.log(err);
                        })
                    for (i = 0; i < me.details.length; i++) {
                        if (me.fixedAssetLicenses.length != 0)
                            if (me.details[i] == me.fixedAssetLicenses[0].LicenseDetailId) {
                                index = i;
                            }
                    }
                    //Xóa id ra khỏi mảng details theo
                    for (i = index; i < (index + me.fixedAssetLicenses.length); i++) {
                        me.details[i] = '';
                    }

                    //Hiển thị lên detail của chứng từ được check trước đó
                    var last = '';
                    for (const id of me.idSelected) {
                        if (id)
                            last = id;
                    }

                    if (last) {
                        var paramAxioss = {
                            licenseId: last
                        }
                        await axios.get(resources.url.host + "/api/v1/LicenseDetails/getDetailAssets", {
                                params: paramAxioss
                            })
                            .then(function (res) {
                                me.fixedAssetLicenses = res.data;
                            }).catch(function (err) {
                                console.log(err);
                            })
                    } else {
                        //Xóa trắng bảng tài sản thuộc chứng từ
                        me.fixedAssetLicenses = [];
                    }
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
                me.isShowMultiDelete=me.isShow;
                //Bôi đậm dòng được check
                for (let i = 0; i < me.licenses.length; i++) {
                    me.checkboxArray[i] = me.checkAll;
                    if (me.checkAll)
                        me.idSelected[i] = me.licenses[i].licenseId;
                    else
                        me.idSelected[i] = '';
                }
                if(!me.checkAll) me.isShowMultiDelete = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gọi api phân trang
         * NDHoang(22/06/2022)
         */
        pagingApi(pageIndex, pageSize, search) {
            var me = this;
            //Gọi api phân trang
            axios.get(resources.url.host + `/api/v1/Licenses/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${search}`)
                .then(function (res) {
                    me.licenses = res.data.data;
                    me.totalPage = Math.ceil(res.data.totalRecord / pageSize);
                    me.totalRecord = res.data.totalRecord;
                    me.recordStart = res.data.recordStart;
                    me.getNewCode();
                })
                .catch(function (res) {
                    console.log(res);
                });
        },

        /**
         * Hàm gán ẩn hiện detail table
         * NDHoang(22/06/2022)
         */
        showDetailTable() {
            if (this.isShowDetail == false) {
                this.isShowDetail = true
            } else this.isShowDetail = false
        },

        /**
         * Hàm gán ẩn hiện detail table
         * NDHoang(22/06/2022)
         */
        showFullDetailTable() {
            if (this.isShowFullDetail == false) {
                this.isShowFullDetail = true
            } else this.isShowFullDetail = false
        },

        /**
         * Hàm gán ẩn hiện modal
         * NDHoang(22/06/2022)
         */
        showModal(showModal) {
            try {
                var me = this
                me.isShowModal = showModal;
                me.licenseDetailIdSelected = '';
                me.resetTable();
            } catch (error) {
                console.log(error);
            }

        },

        /**
         * Hàm gán ẩn hiện chosen modal
         * NDHoang(22/06/2022)
         */
        showChosen(showChosen) {
            try {
                var me = this
                me.isShowChosen = showChosen;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm gán selected detail id
         * NDHoang(22/06/2022)
         */
        getLicenseDetailId(detailIdSelected, index) {
            var me = this;
            me.licenseDetailIdSelected = detailIdSelected;
            me.indexRowDetail = index;
        },

        /**
         * Hàm gán selected detail id
         * NDHoang(22/06/2022)
         */
        getFixedAssetSelected(fixedAsset, index) {
            var me = this;
            me.fixedAssetSelected = fixedAsset;
            me.indexRowDetail = index;
        },

        /**
         * Hàm gán lish assets
         * NDHoang(22/06/2022)
         */
        getListAsset(list) {
            var me = this;
            me.listAsset = list;
        },


        /**
         * Hàm gán ẩn hiện selected modal
         * NDHoang(22/06/2022)
         */
        showSelected(showSelected) {
            try {
                var me = this
                me.isShowSelectedDetail = showSelected;
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
         * Chọn bằng enter
         * NDHoang(22/06/2022)
         */
        selectListByEnter() {
            try {
                var me = this;
                //Ẩn danh sách
                me.isShowListPageSize = me.isHide;
                me.pagingApi(me.page, me.defaultPageSize, me.search);
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
         * tab ra ngoài combobox
         * NDHoang(22/06/2022)
         */
        tabOutCombobox() {
            try {
                var me = this;
                //Ẩn list
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

        /**
         * Sự kiện combobox item on click
         * NDHoang (23/06/2022)
         */

        //Click vào combobox pageSize items
        listTotalRecodsOnClick(Size) {
            try {
                var me = this;
                //Gán giá trị
                me.defaultPageSize = Size;
                //Gọi Api
                me.pagingApi(me.page, me.defaultPageSize, me.search);
                //Ẩn list
                me.isShowListPageSize = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

    },

    /**
     * Tạo đối tượng tìm kiếm
     * NDHoang(22/06/2022)
     */
    watch: {
        //Khi số trang thay đổi
        page(value) {
            var me = this;
            //Gọi Api paging
            me.pagingApi(value, me.defaultPageSize, me.search);
        },

        //Khi pageSize thay đổi
        defaultPageSize(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, value, me.search);
        },

        //Khi ô tìm kiếm thay đổi
        search(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, me.defaultPageSize, value);
        },
    },

    created() {
        //Lấy toàn bộ dữ liệu
        try {
            var me = this;
            me.isLoading = me.isShow;
            //Load lại table
            me.resetTable();
        } catch (error) {
            console.log(error)
        }

    },
}
</script>
