<template>
<div class="content-menu">
    <div class="content-menu-input">
        <div class="input-icon">
            <div class="input-icon1"></div>
            <input maxlength="255" @keyup.enter="selectListByEnter" id="txtSearch" v-model="search" type="text" class="input search-input" placeholder="Tìm kiếm tài sản">
        </div>
        <div class="combobox">
            <div class="combobox-selected">
                <div class="input-icon2"></div>
                <input v-click-outside="onClickOutsideCategoriesCombobox" placeholder="Loại tài sản" @keyup.enter="selectListByEnter" @keydown.tab="tabOutCombobox" @keydown.up="arrowListUp($event,fixedAssetCategoriesFilter)" @keyup.down="arrowListDown(fixedAssetCategoriesFilter)" @keyup="keyUpHandle($event,fixedAssetCategoriesFilter)" autocomplete="off" type="text" v-model="fixedAssetCategoriesDefaulName" class="selected-item" @focusin="fixedAssetCategoriesComboboxOnFocus">
                <div class="input-icon3"></div>
                <div ref="comboboxListCategories" class="combobox-list" :class="{'visible': isShowListCategoriesAssets}">
                    <div ref="optionsListCategories" v-for="(cat,index) in fixedAssetCategoriesFilter" :key="cat.fixedAssetCategoryId" :class="{'ishover': index==arrowCategoriesCounter}" class="combobox-items" @click="listFixedAssetCategoriesOnClick(cat.fixedAssetCategoryName)">
                        <div :title="cat.fixedAssetCategoryName" class="tabletext-wrap">{{cat.fixedAssetCategoryName}}</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="combobox">
            <div class="combobox-selected">
                <div class="input-icon2"></div>
                <input v-click-outside="onClickOutsideDepartmentsCombobox" placeholder="Bộ phận sử dụng" @keyup.enter="selectListByEnter" @keydown.tab="tabOutCombobox" @keydown.up="arrowListUp($event,departmentsFilter)" @keyup.down="arrowListDown(departmentsFilter)" @keyup="keyUpHandle($event,departmentsFilter)" autocomplete="off" type="text" v-model="departmentDefaulName" class="selected-item" @focusin="departmentsComboboxOnFocus">
                <div class="input-icon3"></div>
                <div ref="comboboxDepartments" class="combobox-list" :class="{'visible': isShowListDepartments}">
                    <div ref="optionsDepartments" v-for="(dep,index) in departmentsFilter" :key="dep.DepartmentId" :class="{'ishover': index==arrowDepartmentsCounter}" class="combobox-items" @click="listDepartmentsOnClick(dep.departmentName)">
                        <div class="tabletext-wrap">{{dep.departmentName}}</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-menu-button">
        <button id="btnAdd" class="main-button rectangle-button" @click="addFixedAssetOnClick">
            + Thêm tài sản
        </button>
        <input ref="fileImport" type="file" @change="handleFileUpload( $event )" hidden>
        <button title="Nhập khẩu dữ liệu excel" class="import-button main-button square-button" @click="importOnClick">
        </button>
        <button title="Xóa tài sản được chọn" id="btnDelete" class="discard-button main-button square-button" @click="deleteOnClick">
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
            <td class="column-style-fix9" title="Hao mòn/Khấu hao lũy kế = Nguyên giá * tỉ lệ hao mòn">HM/KH lũy kế</td>
            <td class="column-style-fix10" title="Giá trị còn lại">Giá trị còn lại</td>
            <td class="column-style-fix11" title="Chức năng">Chức năng</td>
        </tr>
    </table>
    <table id="tblListEmployees" class="content-table">
        <tr v-for="(fixedAsset,index) in fixedAssets" :key="fixedAsset.fixedAssetId" class="table-column-infor" @contextmenu="onContextMenu($event,fixedAsset)" @click="clickOnRow(fixedAsset, index)" @dblclick="rowOnDbClick(fixedAsset)" :class="{'row-selected': checkboxArray[index]}">
            <!-- v-model="checked" -->
            <td class="column-style-fix1"><input class="input-checkbox" v-model="checkboxArray[index]" type="checkbox"></td>
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
            <td class="column-style-fix9">{{formatMoney(fixedAsset.cost * fixedAsset.depreciationRate)}}</td>
            <td class="column-style-fix10">{{formatMoney(fixedAsset.cost - fixedAsset.cost * fixedAsset.depreciationRate)}}</td>
            <td class="column-style-fix11">
                <div class="icon-wraper">
                    <div id="addBtn" class="icon-feature icon-feature1" @click.stop="rowOnDbClick(fixedAsset)"></div>
                    <div class="icon-feature icon-feature2" @click.stop="copyAssetOnClick(fixedAsset)"></div>
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
            <td class="column-style-fix7">{{formatMoney(this.totalQuantity)}}</td>
            <td class="column-style-fix8">{{formatMoney(this.totalPrice)}}</td>
            <td class="column-style-fix9">{{formatMoney(this.totalAccumulated)}}</td>
            <td class="column-style-fix10">{{formatMoney(this.totalRemain)}}</td>
            <td class="column-style-fix11"></td>
        </tr>
    </table>
</div>
<FixedAssetDetailVue v-if="isshowModal" :fixedAssetSelected="fixedAssetSelected" :editMode="editMode" @closeModal="showModal" @showSuccessMessage="successMessage" @resetTable="resetTable" />
<HLoading v-if="isLoading" />
<HErrorMessage v-if="isShowErrorNotice" @closeNotice="showErrorNotice" :errorMessage="errorMessage" />
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
</style>

<script>
import resources from '../../js/resource.js'
import enums from '../../js/enum.js';
//Sử dụng axioss
import axios from "axios";
//Sử dụng components view FixedAssetDetail
import FixedAssetDetailVue from "./FixedAssetDetail.vue";
//Sử dụng thư viện paginate
import Paginate from "vuejs-paginate-next";

export default {
    name: "FixedAssetTable",
    //Tạo components
    components: {
        FixedAssetDetailVue,
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
            departmentDefaulName: '',
            fixedAssetCategoriesDefaulName: '',

            //Số lượng bản ghi trên một trang
            defaultPageSize: 20,
            pageSize1: 10,
            pageSize2: 20,
            pageSize3: 30,

            //Đặt mặc định ẩn list
            isShowListDepartments: false,
            isShowListCategoriesAssets: false,
            isShowListPageSize: false,

            //Mặc định ẩn thông báo
            isShowNoticeMessage: false,
            isShowDetailMessage: false,

            //Đặt mặc định ẩn modal
            isshowModal: false,

            //Đặt mặc định ẩn ToastMessage
            isShowSuccess: false,

            //Đặt mặc định ẩn notice
            isShowNotice: false,
            
            //Đặt mặc định ẩn error message
            isShowErrorNotice:false,

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

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //tạo mảng đối tượng tài sản
            fixedAssets: [],
            assets: [],

            //Tạo mảng đối tượng assetCategorys
            fixedAssetCategories: [],
            fixedAssetCategoriesFilter: [],

            //tạo đối tượng fixedAssetSelected
            fixedAssetSelected: {},

            //tạo mảng đối tượng phòng ban,
            departments: [],
            departmentsFilter: [],

            //Tạo biến lưu mã tài sản mới
            newCode: '',

            //tạo mảng lưu id selected
            idSelected: [],

            //Biến lưu nội dung popup
            messageDelete: '',

            //Biến lưu nội dung thông báo
            noticeMessage: [],
            detailMessage: [],
            errorMessage: [],

            //Tạo biến lưu arrowCounter
            arrowDepartmentsCounter: 0,
            arrowCategoriesCounter: 0,

            //Tạo biến lưu file
            file: '',
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
         * Chức năng context menu cho từng tài sản
         * NDHoang (19/07/2022)
         */
        onContextMenu(e, fixedAsset) {
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
                            this.rowOnDbClick(fixedAsset);
                        },
                    },
                    {
                        label: "Xóa",
                        onClick: () => {
                            this.deleteFixedByContextMenu(fixedAsset);
                        },
                    },
                    {
                        label: "Nhân bản",
                        onClick: () => {
                            this.copyAssetOnClick(fixedAsset);
                        },
                    },
                ]
            });
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

        /**
         * Reset Table
         * NDHoang (04/07/2022)
         */
        resetTable() {
            try {
                var me = this;
                //Gọi api phân trang
                axios.get(resources.url.host + `/api/v1/FixedAssets/filter?pageIndex=${me.page}&pageSize=${me.defaultPageSize}&filter=${me.search}&departmentName=${me.departmentDefaulName}&CategoryName=${me.fixedAssetCategoriesDefaulName}`)
                    .then(function (res) {
                        //Gán lại giá trị cho đối tượng
                        me.fixedAssets = res.data.data;
                        me.totalPage = Math.ceil(res.data.totalRecord / me.defaultPageSize);
                        me.totalRecord = res.data.totalRecord;
                        me.recordStart = res.data.recordStart;
                        //Ẩn toast message
                        setTimeout(function () {
                            me.isShowSuccess = me.isHide;
                        }, 2000);
                        //Ẩn loading
                        me.isLoading = me.isHide;
                        //Gán lại các giá trị để tính giá trị mới
                        me.totalPrice = 0;
                        me.totalQuantity = 0;
                        me.totalAccumulated = 0;
                        me.totalRemain = 0;
                        //Tính toán lại các giá trị
                        for (const asset of me.fixedAssets) {
                            asset.depreciationRate = (1 / asset.lifeTime).toFixed(2);
                            asset.depreciationValueYear = asset.depreciationRate * asset.cost;
                            me.totalPrice += asset.cost;
                            me.totalQuantity += asset.quantity;
                            me.totalAccumulated += asset.depreciationValueYear
                            me.totalRemain += asset.cost - asset.depreciationValueYear;
                        }
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
            axios.get(resources.url.host + "/api/v1/FixedAssets/NewFixedAssetCode")
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
                //Làm rỗng fixedAssetSelected
                me.fixedAssetSelected = {};
                //Format lại mã tài sản rồi gán lại
                me.fixedAssetSelected.fixedAssetCode = me.newCode;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * xử lí file đầu vào
         * NDHoang (19/07/2022)
         */
        handleFileUpload(event) {
            try {
                var me = this
                //Khởi tạo đối tượng file từ input
                me.file = event.target.files[0];
                //Khởi tạo biến kiểu formData chuẩn bị import
                let formData = new FormData();
                //Truyền đối tượng file vào FormData
                formData.append('fileImport', me.file);
                //Gọi API import
                axios.post(resources.url.host + "/api/v1/fixedAssets/import", formData)
                    .then(function (res) {
                        if (res) {
                            //Đếm số lượng bản ghi import
                            var couterImport = 0;
                            var couterUnImport = 0;
                            //Làm rỗng mảng detail
                            me.detailMessage = [];
                            //Duyệt tất cả data
                            for (let data of res.data) {
                                if (data.isValidImport == true) {
                                    couterImport++;
                                } else {
                                    couterUnImport++;
                                    //duyệt tất cả data lỗi
                                    if (couterUnImport > 0) {
                                        for (let errorImport of data.listErrorImport)
                                            me.detailMessage.push(resources.import.beginText + `${data.fixedAssetCode}` + `: ${errorImport}`);
                                    }
                                }
                            }
                            //Nếu thành công tất cả gán lại giá trị cho detailMessage
                            if (couterImport == res.data.length) {
                                me.detailMessage.push(resources.import.isValid);
                            }
                            //Làm rỗng mảng noticeMessage
                            me.noticeMessage = [];
                            //Gán thông báo
                            me.noticeMessage.push(`${couterImport}` + resources.import.couterImport);
                            me.noticeMessage.push(`${couterUnImport}` + resources.import.couterUnImport);
                            //Hiện thông náo
                            me.showNoticeMessage(me.isShow);
                            //Loading
                            me.isLoading = me.isShow;
                            //Gán lại giá trị rỗng cho file target
                            me.resetTable();
                        }
                    })
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
                var count = 0;
                for (const id of this.idSelected) {
                    if (id) {
                        count++;
                    }
                }
                //Điều kiện xóa
                if (count == 0) {
                    return;
                } else if (count == 1) {
                    this.messageDelete = resources.delete.beginText + `${this.fixedAssetSelected.fixedAssetCode}-${this.fixedAssetSelected.fixedAssetName}` + resources.delete.endText;
                } else if (count < 10) {
                    this.messageDelete = resources.import.isZero + count + resources.delete.mutilDelete;
                } else {
                    this.messageDelete = count + resources.delete.mutilDelete;
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
        deleteFixedAsset() {
            try {
                var me = this;
                //Đổi trạng thái của editMode
                me.editMode = enums.editMode.deleteMode;
                var listIdSelected = me.idSelected.join(',');
                //Xóa tất cả tài sản trong mảng
                axios.delete(resources.url.host + `/api/v1/FixedAssets/filterId?fixed_Asset_ids=${listIdSelected}`)
                    .then(function () {
                        me.successMessage(me.isShow);
                        me.isLoading = me.isShow;
                        me.resetTable();
                    })
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
                //Làm rỗng mảng xóa
                me.idSelected = [];
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
        deleteFixedByContextMenu(fixedAsset) {
            try {
                var me = this;
                //Gán lại mảng rỗng
                me.idSelected = [];
                //Đổi trạng thái của editMode
                me.editMode = enums.editMode.deleteMode;
                //Gán đối tượng selected
                me.fixedAssetSelected = fixedAsset;
                //Thêm id vào mảng
                me.idSelected.push(fixedAsset.fixedAssetId);
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
        rowOnDbClick(fixedAsset) {
            try {
                var me = this;
                //Hiện modal
                me.showModal(me.isShow);
                //Gán lại đối tượng selected
                me.fixedAssetSelected = fixedAsset;
                //Đổi form mode
                me.editMode = enums.editMode.updateMode;
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
                //Gán lại đối tượng selected
                me.fixedAssetSelected = fixedAsset;
                me.showModal(me.isShow);
                //Đổi form mode
                me.editMode = enums.editMode.insertMode;
                //Lấy mã tài sản mới
                me.fixedAssetSelected.fixedAssetCode = me.newCode;
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
                if (me.checkboxArray[index] == me.isChecked) {
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
                    me.idSelected[index] = fixedAsset.fixedAssetId;
                    if (me.idSelected.length == me.fixedAssets.length) {
                        me.checkAll = me.isShow;
                    }
                } else {
                    me.isHighLine = false;
                    for (var i = 0; i < me.checkboxArray.length; i++) {
                        if (me.checkboxArray[i]) {
                            me.fixedAssetSelected = me.fixedAssets[i];
                        }
                    }
                    //Xóa phần tử không check ra khỏi mảng
                    me.idSelected[index] = '';
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
                for (let i = 0; i < me.fixedAssets.length; i++) {
                    me.checkboxArray[i] = me.checkAll;
                    if (me.checkAll)
                        me.idSelected.push(me.fixedAssets[i].fixedAssetId);
                    else
                        me.idSelected.pop();
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
            //Gọi api phân trang
            axios.get(resources.url.host + `/api/v1/FixedAssets/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${search}&departmentName=${departmentName}&CategoryName=${categoryName}`)
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
                        asset.depreciationRate = (1 / asset.lifeTime).toFixed(2);
                        asset.depreciationValueYear = asset.depreciationRate * asset.cost;
                        me.totalPrice += asset.cost;
                        me.totalQuantity += asset.quantity;
                        me.totalAccumulated += asset.depreciationValueYear;
                        me.totalRemain += asset.cost - asset.depreciationValueYear;
                    }
                    me.getNewCode();
                })
                .catch(function (res) {
                    console.log(res);
                });
        },

        /**
         * Hàm gán ẩn hiện modal
         * NDHoang(22/06/2022)
         */
        showModal(showModal) {
            try {
                var me = this
                me.isshowModal = showModal;
                me.resetTable();
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
         * Sự kiện của các phím sử dụng trong combobox category
         * NDHoang(22/06/2022)
         */

        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowListDown(object) {
            try {
                var me = this;
                //Hiện danh sách loại tài sản
                if (object == this.fixedAssetCategoriesFilter) {
                    me.isShowListCategoriesAssets = me.isShow;
                    //Điều kiện chọn
                    if (me.arrowCategoriesCounter < object.length - 1)
                        me.arrowCategoriesCounter++;
                    //Autocomplete
                    me.fixedAssetCategoriesDefaulName = object[me.arrowCategoriesCounter].fixedAssetCategoryName;
                    //Gọi hàm scroll
                    me.scrollListCategoriesTo();
                } else {
                    //Hiển thị danh sách
                    me.isShowListDepartments = me.isShow;
                    //Điều kiện chọn
                    if (me.arrowDepartmentsCounter < object.length - 1)
                        me.arrowDepartmentsCounter++;
                    //Autocomplete
                    me.departmentDefaulName = object[me.arrowDepartmentsCounter].departmentName;
                    me.scrollDepartmentsTo();
                }

            } catch (error) {
                console.log(error);
            }
        },
        // Dùng phím mũi tên lên để di chuyển lựa chọn
        arrowListUp(e, object) {
            try {
                e.preventDefault();
                var me = this;
                //Hiện danh sách loại tài sản
                if (object == me.fixedAssetCategoriesFilter) {
                    me.isShowListCategoriesAssets = me.isShow;
                    //Điều kiện chọn
                    if (me.arrowCategoriesCounter > 0)
                        me.arrowCategoriesCounter--;
                    //Autocomplete
                    me.fixedAssetCategoriesDefaulName = object[me.arrowCategoriesCounter].fixedAssetCategoryName;
                    //Gọi hàm scroll
                    me.scrollListCategoriesTo();
                } else {
                    //Hiển thị danh sách
                    me.isShowListDepartments = me.isShow;
                    //Điều kiện chọn
                    if (me.arrowDepartmentsCounter > 0)
                        me.arrowDepartmentsCounter--;
                    //Autocomplete
                    me.departmentDefaulName = object[me.arrowDepartmentsCounter].departmentName;
                    me.scrollDepartmentsTo();
                }
            } catch (error) {
                console.log();
            }
        },

        /**
         * Cuộn list bằng phím
         * NDHoang(22/06/2022)
         */
        scrollListCategoriesTo() {
            //Tạo biến ghi lại chiều cao của danh sách
            const itemHeight = this.$refs.optionsListCategories[this.arrowCategoriesCounter].clientHeight;
            //Cuộn theo arrowCounter
            this.$refs.comboboxListCategories.scrollTop = itemHeight * this.arrowCategoriesCounter - this.$refs.comboboxListCategories.clientHeight / 2;
        },

        scrollDepartmentsTo() {
            //Tạo biến ghi lại chiều cao của danh sách
            const itemHeight = this.$refs.optionsDepartments[this.arrowDepartmentsCounter].clientHeight;
            //Cuộn theo arrowCounter
            this.$refs.comboboxDepartments.scrollTop = itemHeight * this.arrowDepartmentsCounter - this.$refs.comboboxDepartments.clientHeight / 2;
        },

        /**
         * Chọn bằng enter
         * NDHoang(22/06/2022)
         */
        selectListByEnter() {
            try {
                var me = this;
                //Ẩn danh sách
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
                me.isShowListPageSize = me.isHide;
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Click ra ngoài combobox
         * NDHoang(22/06/2022)
         */
        //Sự kiên click ra ngoài combobox categories
        onClickOutsideCategoriesCombobox() {
            try {
                var me = this;
                //Ẩn danh sách
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        //Sự kiên click ra ngoài combobox department
        onClickOutsideDepartmentsCombobox() {
            try {
                var me = this;
                //Ẩn list
                me.isShowListDepartments = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

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
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
                me.isShowListPageSize = me.isHide;
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
                //Ẩn hiện list
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
                //Ẩn hiện list
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
        //Click vào combobox bộ phận sử dụng items
        listDepartmentsOnClick(departmentName) {
            try {
                var me = this;
                //Gán giá trị
                me.departmentDefaulName = departmentName;
                //Ẩn list
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
            } catch (error) {
                console.log(error);
            }
        },

        //Click vào combobox loại tài sản items
        listFixedAssetCategoriesOnClick(fixedAssetCategoryName) {
            try {
                var me = this;
                //Gán giá trị
                me.fixedAssetCategoriesDefaulName = fixedAssetCategoryName;
                //Ẩn list
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, me.fixedAssetCategoriesDefaulName);
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
         * Lọc danh sách list theo input
         * NDHoang(26/07/2022)
         */
        categoriesFilterHandel() {
            try {
                var me = this;
                if (!me.fixedAssetCategoriesDefaulName) {
                    me.fixedAssetCategoriesFilter = me.fixedAssetCategories
                } else {
                    me.fixedAssetCategoriesFilter = me.fixedAssetCategories.filter((Category) => {
                        return Category.fixedAssetCategoryName.toLowerCase().match(me.fixedAssetCategoriesDefaulName.toLowerCase());
                    });
                }
                return me.fixedAssetCategoriesFilter;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Lọc danh sách list theo input
         * NDHoang(26/07/2022)
         */
        departmentsFilterHandel() {
            try {
                var me = this;
                if (!me.departmentDefaulName) {
                    me.departmentsFilter = me.departments
                } else {
                    me.departmentsFilter = me.departments.filter((department) => {
                        return department.departmentName.toLowerCase().match(me.departmentDefaulName.toLowerCase());
                    });
                }
                return me.departmentsFilter;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Bắt sự kiện phím điều hướng để thực hiện autocomplete
         * NDHoang(26/07/2022)
         */
        keyUpHandle(e, object) {
            try {
                var me = this;
                var keyCode = e.which;
                // Thực hiện lọc list nếu giá trị keyCode không phải phím điều hướng lên xuống hoặc enter
                if (keyCode != enums.keyCode.arrowUp && keyCode != enums.keyCode.arrowDown && keyCode != enums.keyCode.enter && keyCode != enums.keyCode.esc) {
                    me.arrowDepartmentsCounter = 0;
                    me.arrowCategoriesCounter = 0;
                    if (object == me.departmentsFilter) {
                        me.isShowListDepartments = me.isShow;
                        me.departmentsFilterHandel();
                    } else {
                        me.isShowListCategoriesAssets = me.isShow;
                        me.categoriesFilterHandel();
                    }
                }
            } catch (error) {
                console.log(error);
            }

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

        //Khi chọn combobox bộ phận sử dụng
        departmentDefaulName(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, me.defaultPageSize, me.search, value, me.fixedAssetCategoriesDefaulName);
        },
        //Khi chọn combobox loại tài sản
        fixedAssetCategoriesDefaulName(value) {
            var me = this;
            //Gọi Api paging
            if (value == '')
                me.pagingApi(me.page, me.defaultPageSize, me.search, me.departmentDefaulName, value);
        }
    },

    created() {
        //Lấy toàn bộ dữ liệu
        try {
            var me = this;
            me.isLoading = me.isShow;
            //Load lại table
            me.resetTable();
            //Lấy danh sách phòng ban
            axios.get(resources.url.host + "/api/v1/Departments")
                .then(function (res) {
                    me.departments = res.data;
                    me.departmentsFilterHandel();
                })
                .catch(function (res) {
                    console.log(res);
                })
            //Lấy danh sách loại tài sản
            axios.get(resources.url.host + "/api/v1/FixedAssetCategories")
                .then(function (res) {
                    me.fixedAssetCategories = res.data;
                    me.categoriesFilterHandel();
                })
                .catch(function (res) {
                    console.log(res);
                })

        } catch (error) {
            console.log(error)
        }

    },
}
</script>
