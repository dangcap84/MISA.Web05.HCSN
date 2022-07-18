<template>
<div id="dbEpl" class="modal" @keyup.esc="closeModalOnclick">
    <div class="modal-layer"></div>
    <div class="modal-body">
        <div class="modal-body-title">
            <div class="modal-body-title_text">Chi tiết tài sản</div>
            <div id="closeBtn" class="close-icon" @click="closeModalOnclick"></div>
        </div>
        <div class="modal-body-input">
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Mã tài sản
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input ref="txtAssetCodeRef" v-model="fixedAsset.fixedAssetCode" type="text" class="input modal-input_style-left" placeholder="Nhập mã tài sản" :class="{'not-valid': inValid.validFixedAssetCode}">
                    </div>
                    <div :class="{'visible': inValid.validFixedAssetCode}" id="txtErrorEmployeeCode" class="input-validate-error">Mã tài sản không được để trống</div>
                </div>
                <div class="body-input_right validate-text">
                    <div class="body-input-text">Tên tài sản
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="fixedAsset.fixedAssetName" type="text" class="input input-modal_style-right" placeholder="Nhập tên tài sản" :class="{'not-valid': inValid.validFixedAssetName}">
                    </div>
                    <div :class="{'visible': inValid.validFixedAssetName}" class="input-validate-error">Tên tài sản không được để trống</div>
                </div>
            </div>
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Mã bộ phận sử dụng
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <!-- <input id="txtdepartmentCode" v-model="fixedAsset.departmentCode" propName="departmentID" type="text" class="input modal-input_style-left" placeholder="Nhập mã bộ phận" :class="{'not-valid': inValid.validDepartmentCode}"> -->
                        <div class="combobox combobox-modal-style">
                            <div class="combobox-selected " :class="{'not-valid': inValid.validDepartmentCode,'onfocus': isShowListDepartments}">
                                <input @keyup.enter="selectDepartmentsByEnter(departments)" @keyup.up="arrowDepartmentsUp(departments)" @keyup.down="arrowDepartmentsDown(departments)" autocomplete="off" type="text" v-model="fixedAsset.departmentCode" class="selected-item" placeholder="Nhập mã bộ phận" @focusin="departmentsComboboxOnFocus">
                                <div class="input-icon3"></div>
                                <div class="combobox-list" :class="{'visible': isShowListDepartments}">
                                    <div v-for="(dep,index) in departments" :key="dep.departmentId" :class="{'ishover': index==arrowDepartmentsCounter}" class="combobox-items" :value="dep.departmentId" @click="listDepartmentsOnClick(dep)">{{dep.departmentCode}} -- {{dep.departmentName}}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div :class="{'visible': inValid.validDepartmentCode}" id="txtErrordepartmentCode" class="input-validate-error">Mã bộ phận sử dụng không được để trống</div>
                </div>
                <div class="body-input_right validate-text">
                    <div class="body-input-text">Tên bộ phận sử dụng
                    </div>
                    <div class="input-icon">
                        <input v-model="fixedAsset.departmentName" type="text" class="input input-modal_style-right input-disable" disabled>
                    </div>
                </div>
            </div>
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Mã loại tài sản
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <div class="combobox combobox-modal-style">
                            <div class="combobox-selected" :class="{'not-valid': inValid.validFixedAssetCategoryCode,'onfocus': isShowListCategoriesAssets}">
                                <input id="cbxTypeOf" @keyup.enter="selectListCategoriesByEnter(fixedAssetCategories)" @keyup.up="arrowListCategoriesUp(fixedAssetCategories)" @keyup.down="arrowListCategoriesDown(fixedAssetCategories)" autocomplete="off" type="text" v-model="fixedAsset.fixedAssetCategoryCode" class="selected-item" placeholder="Nhập mã bộ phận" @focusin="fixedAssetCategoriesComboboxOnFocus">
                                <div class="input-icon3"></div>
                                <div class="combobox-list" :class="{'visible': isShowListCategoriesAssets}" ref="comboboxMenu">
                                    <div v-for="(cat,index) in fixedAssetCategories" :key="cat.fixedAssetCategoryId" :class="{'ishover': index==arrowCategoriesCounter}" class="combobox-items" :value="cat.fixedAssetCategoryId" @click="listfixedAssetCategoriesOnClick(cat)">{{cat.fixedAssetCategoryCode}}--{{cat.fixedAssetCategoryName}}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div :class="{'visible': inValid.validFixedAssetCategoryCode}" id="txtErrorTypeCode" class="input-validate-error">Mã loại tài sản không được để trống</div>
                </div>
                <div class="body-input_right validate-text">
                    <div class="body-input-text">Tên loại tài sản
                    </div>
                    <div class="input-icon">
                        <input v-model="fixedAsset.fixedAssetCategoryName" type="text" class="input input-modal_style-right input-disable" disabled>
                    </div>
                </div>
            </div>
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Số lượng
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueQuantity" v-money="money" type="text" class="input modal-input_style-left input-quality_style modal-input_style-special" :class="{'not-valid': inValid.validQuantity}">
                        <div class="input-icon4"></div>
                    </div>
                    <div :class="{'visible': inValid.validQuantity}" id="txtErrorQuality" class="input-validate-error">Số lượng không được để trống</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Nguyên giá
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueCost" v-money="money" type="text" class="input modal-input_style-left input-quality_style" :class="{'not-valid': inValid.validCost}">
                    </div>
                    <div :class="{'visible': inValid.validCost}" id="txtErrorOriginPrice" class="input-validate-error">Nguyên giá không được để trống</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Số năm sử dụng
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueLifeTime" v-money="money" type="text" class="input modal-input_style-left input-quality_style" :class="{'not-valid': inValid.validLifeTime}">
                    </div>
                    <div :class="{'visible': inValid.validLifeTime}" id="txtErrorTimeOfUsing" class="input-validate-error">Số năm sử dụng không được để trống</div>
                </div>
            </div>
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Ngày mua
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <Datepicker format="dd/MM/yyyy" arrowNavigation locale="vi" cancelText="" selectText="Chọn" v-model="fixedAsset.purchaseDate" :class="{'not-valid': inValid.validPurchaseDate}">
                            <template #input-icon>
                                <div class="input-icon5"></div>
                            </template>
                        </Datepicker>
                    </div>
                    <div :class="{'visible': inValid.validPurchaseDate}" id="txtErrorByingDate" class="input-validate-error">Ngày mua không được để trống</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Ngày bắt đầu sử dụng
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <Datepicker format="dd/MM/yyyy" arrowNavigation locale="vi" cancelText="" selectText="Chọn" v-model="fixedAsset.productionDate" :class="{'not-valid': inValid.validProductionDate}">
                            <template #input-icon>
                                <div class="input-icon5"></div>
                            </template>
                        </Datepicker>
                    </div>
                    <div :class="{'visible': inValid.validProductionDate}" id="txtErrorUsingDate" class="input-validate-error">Ngày bắt đầu sử dụng không được để trống</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Năm theo dõi
                    </div>
                    <div class="input-icon">
                        <input type="text" value="2022" class="input modal-input_style-left input-quality_style input-disable" disabled>
                    </div>
                </div>
            </div>
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Tỉ lệ hao mòn (%)
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueDepreciationRate" v-money="float" type="text" class="input modal-input_style-left input-quality_style modal-input_style-special" :class="{'not-valid': inValid.validDepreciationRate}">
                        <div class="input-icon4"></div>
                    </div>
                    <div :class="{'visible': inValid.validDepreciationRate}" class="input-validate-error">Tỉ lệ hao mòn(%) không được để trống</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Giá trị hao mòn năm
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueDepreciationValueYear" v-money="money" type="text" class="input modal-input_style-left input-quality_style" :class="{'not-valid': inValid.validDepreciationValueYear}">
                    </div>
                    <div :class="{'visible': inValid.validDepreciationValueYear}" class="input-validate-error">Giá trị hao mòn năm không được để trống</div>
                </div>
                <div class="body- validate-textinput_left" style="display:none">
                </div>
            </div>
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
@import url(../../css/base/datepicker.css);
</style>

<script>
//sử dụng axioss
import axios from 'axios';
//Sử dụng thư viên V-money
import {
    VMoney
} from 'v-money';
//Sử dụng thư viện datepicker
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

export default {
    name: "EmployeeDetail",
    emits: ["closeModal", "resetTable", "showSuccessMessage"],
    components: {
        Datepicker
    },
    directives: {
        money: VMoney,
        float: VMoney
    },

    data() {
        return {
            //Tạo đối tượng money của v-money
            money: {
                decimal: ',',
                thousands: '.',
                precision: 0,
            },
            //Tạo đối tượng v-money kiểu float cho tỉ lệ hao mòn
            float: {
                decimal: ',',
                thousands: '.',
                precision: 2,
            },

            //Tạo các biến v-modal tương tứng với v-money
            valueCost: 0,
            valueDepreciationValueYear: 0,
            valueQuantity: 0,
            valueLifeTime: 0,
            valueDepreciationRate: 0,

            //tạo đối tượng tài sản
            fixedAsset: {},

            //tạo mảng phòng ban
            departments: [],

            //tạo mảng loại tài sản
            fixedAssetCategories: [],

            //Set mặc định ẩn list
            isShowListDepartments: false,
            isShowListCategoriesAssets: false,

            //Set mặc định ẩn cảnh báo
            isShowPostNotice: false,
            isShowPutNotice: false,
            isShowErrorNotice: false,

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //Tạo biến lưu trạng thái form
            insertMode: 0,
            updateMode: 1,
            deleteMode: 2,

            //Tạo đối tượng lưu trữ idSelected
            departmentId: "",
            fixedAssetCategoryId: "",

            //Đặt giá trị mặc định cho mỗi trường validate
            inValid: {
                validFixedAssetCode: false,
                validFixedAssetName: false,
                validDepartmentCode: false,
                validFixedAssetCategoryCode: false,
                validQuantity: false,
                validCost: false,
                validLifeTime: false,
                validDepreciationRate: false,
                validDepreciationValueYear: false,
                validPurchaseDate: false,
                validProductionDate: false,
            },

            //Tạo biến check valid
            isValid: true,

            //Tạo biến chứa error message
            errorMessage: 'Lỗi xảy ra',

            //Tạo biến arrow counter
            arrowCategoriesCounter: 0,
            arrowDepartmentsCounter: 0,
        }
    },

    /**
     * Khai báo prop sử dụng bên ngoài
     */
    props: {
        fixedAssetSelected: {
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
         * Xử lý ngày
         * NDHoang (15/06/2022)
         * */
        formatDate(dob) {
            let dateConvert = new Date(dob);
            if (dob && (dateConvert instanceof Date && !isNaN(dateConvert.valueOf()))) {
                // xử lý ngày
                let date = dateConvert.getDate();
                date = date < 10 ? `0${date}` : date;
                // xử lý tháng
                let month = dateConvert.getMonth();
                month = month < 10 ? `0${month}` : month;
                // xử lý năm
                let year = dateConvert.getFullYear();
                dob = `${year}-${month}-${date}`;
                return dob;
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

        //Hiển thị cảnh báo trước khi tắt
        closeModalOnclick() {
            try {
                var me = this
                //Xét trạng thái modal
                if (me.editMode == me.insertMode) {
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
                if (me.editMode == me.insertMode) {
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
         * Sự kiện của các phím sử dụng trong combobox category
         * NDHoang(22/06/2022)
         */

        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowListCategoriesDown(cat) {
            try {
                var me = this
                //Hiện list danh sách loại tài sản
                me.isShowListCategoriesAssets = me.isShow;
                //So sánh mảng couter với chiều dài của mảng loại tài sản
                if (me.arrowCategoriesCounter < me.fixedAssetCategories.length - 1)
                    me.arrowCategoriesCounter++;
                //autocomplete
                me.fixedAsset.fixedAssetCategoryId = cat[me.arrowCategoriesCounter].fixedAssetCategoryId;
                me.fixedAsset.fixedAssetCategoryCode = cat[me.arrowCategoriesCounter].fixedAssetCategoryCode;
                me.fixedAsset.fixedAssetCategoryName = cat[me.arrowCategoriesCounter].fixedAssetCategoryName;
            } catch (error) {
                console.log(error);
            }
        },
        // Dùng phím mũi tên lên để di chuyển lựa chọn
        arrowListCategoriesUp(cat) {
            try {
                var me = this
                //Hiện list danh sách loại tài sản
                me.isShowListCategoriesAssets = me.isShow;
                //So sánh mảng couter với chiều dài của mảng loại tài sản
                if (me.arrowCategoriesCounter > 0)
                    me.arrowCategoriesCounter--;
                //autocomplete
                me.fixedAsset.fixedAssetCategoryId = cat[me.arrowCategoriesCounter].fixedAssetCategoryId;
                me.fixedAsset.fixedAssetCategoryCode = cat[me.arrowCategoriesCounter].fixedAssetCategoryCode;
                me.fixedAsset.fixedAssetCategoryName = cat[me.arrowCategoriesCounter].fixedAssetCategoryName;
            } catch (error) {
                console.log(error);
            }
        },
        //Chọn bằng enter
        selectListCategoriesByEnter(cat) {
            try {
                var me = this;
                //Gán giá trị mặc định số năm sử dụng của loại tài sản
                me.valueLifeTime = cat[this.arrowCategoriesCounter].lifeTime;
                //Tỉ lệ hao mòn bằng 1 chia số năm sử dụng
                var num = (1 / cat[this.arrowCategoriesCounter].lifeTime) * 100;
                //Tỉ lệ hao mòn làm tròn tới số thập phân thứ 2
                me.valueDepreciationRate = num.toFixed(2);
                //Ẩn danh sách
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện của các phím sử dụng trong combobox department
         * NDHoang(22/06/2022)
         */

        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowDepartmentsDown(dep) {
            try {
                var me = this;
                //Hiện list danh sách phòng ban
                me.isShowListDepartments = me.isShow;
                //So sánh mảng couter với chiều dài của mảng phòng ban
                if (me.arrowDepartmentsCounter < me.fixedAssetCategories.length - 1)
                    me.arrowDepartmentsCounter++;
                //Autocomplete
                me.fixedAsset.departmentId = dep[me.arrowDepartmentsCounter].departmentId;
                me.fixedAsset.departmentCode = dep[me.arrowDepartmentsCounter].departmentCode;
                me.fixedAsset.departmentName = dep[me.arrowDepartmentsCounter].departmentName;
            } catch (error) {
                console.log(error);
            }
        },
        // Dùng phím mũi tên lên để di chuyển lựa chọn
        arrowDepartmentsUp(dep) {
            try {
                var me = this
                //Hiện list danh sách phòng ban
                me.isShowListDepartments = me.isShow;
                //So sánh mảng couter với chiều dài của mảng phòng ban
                if (me.arrowDepartmentsCounter > 0)
                    me.arrowDepartmentsCounter--;
                //Autocomplete
                me.fixedAsset.departmentId = dep[me.arrowDepartmentsCounter].departmentId;
                me.fixedAsset.departmentCode = dep[me.arrowDepartmentsCounter].departmentCode;
                me.fixedAsset.departmentName = dep[me.arrowDepartmentsCounter].departmentName;
            } catch (error) {
                console.log(error);
            }
        },
        //Chọn bằng enter
        selectDepartmentsByEnter() {
            try {
                var me = this;
                //Ẩn list
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
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
         * Sự kiện onfocus input combobox
         * NDHoang (19/06/2022)
         */
        //Focus on combobox loại tài sản
        fixedAssetCategoriesComboboxOnFocus() {
            try {
                var me = this;
                //Ẩn list phòng ban
                me.isShowListDepartments = me.isHide;
                //Hiện list loại tài sản
                me.isShowListCategoriesAssets = me.isShow;
            } catch (error) {
                console.log(error);
            }
        },

        //Focus on combobox Bộ phận sử dụng
        departmentsComboboxOnFocus() {
            try {
                var me = this;
                //Ẩn list loại tài sản
                me.isShowListCategoriesAssets = me.isHide;
                //Hiện list phòng ban
                me.isShowListDepartments = me.isShow;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện combobox item on click
         * NDHoang (23/06/2022)
         */
        //Click vào combobox bộ phận sử dụng items
        listDepartmentsOnClick(dep) {
            try {
                var me = this;
                //Gán các giá trị tương ứng
                me.fixedAsset.departmentId = dep.departmentId;
                me.fixedAsset.departmentCode = dep.departmentCode;
                me.fixedAsset.departmentName = dep.departmentName;
                //Ẩn list
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        //Click vào combobox loại tài sản items
        listfixedAssetCategoriesOnClick(cat) {
            try {
                var me = this;
                var num = 0
                // Gán các giá trị tương ứng
                me.fixedAsset.fixedAssetCategoryId = cat.fixedAssetCategoryId;
                me.fixedAsset.fixedAssetCategoryCode = cat.fixedAssetCategoryCode;
                me.fixedAsset.fixedAssetCategoryName = cat.fixedAssetCategoryName;
                me.valueLifeTime = cat.lifeTime;
                //Tỉ lệ hao mòn bằng 1 chia số năm sử dụng
                num = (1 / cat.lifeTime) * 100;
                //Tỉ lệ hao mòn làm tròn tới số thập phân thứ 2
                me.valueDepreciationRate = num.toFixed(2);
                //Ẩn list
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
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
                //Tạo đối tượng mới
                const newAsset = {
                    fixedAssetId: me.fixedAsset.fixedAssetId,
                    fixedAssetCode: me.fixedAsset.fixedAssetCode,
                    fixedAssetName: me.fixedAsset.fixedAssetName,
                    departmentId: me.fixedAsset.departmentId,
                    departmentCode: me.fixedAsset.departmentCode,
                    departmentName: me.fixedAsset.departmentName,
                    fixedAssetCategoryId: me.fixedAsset.fixedAssetCategoryId,
                    fixedAssetCategoryCode: me.fixedAsset.fixedAssetCategoryCode,
                    fixedAssetCategoryName: me.fixedAsset.fixedAssetCategoryName,
                    cost: me.fixedAsset.cost,
                    lifeTime: me.fixedAsset.lifeTime,
                    quantity: me.fixedAsset.quantity,
                    productionDate: me.fixedAsset.productionDate,
                    purchaseDate: me.fixedAsset.purchaseDate,
                    depreciationRate: me.fixedAsset.depreciationRate,
                    depreciationValueYear: me.fixedAsset.depreciationValueYear
                }
                //Validate dữ liệu
                if (!me.validate()) return;
                else {
                    if (me.editMode == me.insertMode) {
                        try {
                            //Post đối tượng
                            axios.post("http://localhost:64168/api/v1/fixedAssets", newAsset)
                                .catch(function (error) {
                                    if (error.response) {
                                        //Hiển thị popup lỗi
                                        me.errorMessage = "Mã tài sản đã tồn tại trong hệ thống";
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
                            axios.put(`http://localhost:64168/api/v1/fixedAssets`, newAsset)
                                .catch(function (error) {
                                    if (error.response) {
                                        //Hiển thị popup lỗi
                                        me.errorMessage = "Mã nhân viên đã tồn tại trong hệ thống";
                                        me.showErrorNotice(me.isShow);
                                        console.log(error.response.data);
                                        console.log(error.response.status);
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
            //Check mã tài sản
            try {
                this.isValid = true;
                if (!this.fixedAsset.fixedAssetCode) {
                    this.isValid = false;
                    this.inValid.validFixedAssetCode = true;
                } else {
                    this.inValid.validFixedAssetCode = false;
                }
                //Check tên tài sản
                if (!this.fixedAsset.fixedAssetName) {
                    this.isValid = false;
                    this.inValid.validFixedAssetName = true;
                } else {
                    this.inValid.validFixedAssetName = false;
                }
                //Check mã bộ phận sử dụng
                if (!this.fixedAsset.departmentCode) {
                    this.isValid = false;
                    this.inValid.validDepartmentCode = true;
                } else {
                    this.inValid.validDepartmentCode = false;
                }
                //Check mã loại tài sản
                if (!this.fixedAsset.fixedAssetCategoryCode) {
                    this.isValid = false;
                    this.inValid.validFixedAssetCategoryCode = true;
                } else {
                    this.inValid.validFixedAssetCategoryCode = false;
                }
                //Check số lượng
                if (!this.valueQuantity) {
                    this.isValid = false;
                    this.inValid.validQuantity = true;
                } else {
                    this.inValid.validQuantity = false;
                }
                //Check nguyên giá
                if (!this.valueCost) {
                    this.isValid = false;
                    this.inValid.validCost = true;
                } else {
                    this.inValid.validCost = false;
                }
                //Số năm sử dụng
                if (!this.valueLifeTime) {
                    this.isValid = false;
                    this.inValid.validLifeTime = true;
                } else {
                    this.inValid.validLifeTime = false;
                }
                //Check tỉ lệ hao mòn
                if (!this.valueDepreciationRate) {
                    this.isValid = false;
                    this.inValid.validDepreciationRate = true;
                } else {
                    this.inValid.validDepreciationRate = false;
                }
                //Check giá trị hao mòn 
                if (!this.valueDepreciationValueYear) {
                    this.isValid = false;
                    this.inValid.validDepreciationValueYear = true;
                } else {
                    this.inValid.validDepreciationValueYear = false;
                }
                //Check ngày mua
                if (!this.fixedAsset.purchaseDate) {
                    this.isValid = false;
                    this.inValid.validPurchaseDate = true;
                } else {
                    this.inValid.validPurchaseDate = false;
                }
                //Check ngày sử dụng
                if (!this.fixedAsset.productionDate) {
                    this.isValid = false;
                    this.inValid.validProductionDate = true;
                } else {
                    this.inValid.validProductionDate = false;
                }
                if (!this.isValid) {
                    return false;
                } else return true;
                
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Xử lý dữ convert sang int
         * NDHoang(05/07/2022)
         */
        convertIntNumber(num) {
            //Ép kiểu cho num
            num = parseInt(String(num).replaceAll(/\D+/g, ''))
            return num;
        },

    },

    /**
     * Xử lý dữ liệu số nhập vào
     * NDHoang(05/07/2022)
     */
    watch: {
        valueDepreciationRate(value) {
            //Loại bỏ các dấu cách mặc định của chuỗi
            value = String(value).replaceAll(/\./g, '');
            value = String(value).replaceAll(/,/g, '.');
            //Ép lại kiểu cho value
            value = parseFloat(value);
            // So sánh giá trị với max
            this.fixedAsset.depreciationRate = value/100;
        },

        valueCost(p) {
            //convert p sang dạng int
            this.fixedAsset.cost = this.convertIntNumber(p);
            //Công thức tính giá trị hao mòn năm nguyên giá nhân với tỉ lệ hao mòn
            var total = this.fixedAsset.cost * this.fixedAsset.depreciationRate;
            this.valueDepreciationValueYear = Math.floor(total);
        },

        valueDepreciationValueYear(p) {
            //convert p sang dạng int
            this.fixedAsset.depreciationValueYear = this.convertIntNumber(p)
        },

        valueQuantity(p) {
            //convert p sang dạng int
            this.fixedAsset.quantity = this.convertIntNumber(p)
        },

        valueLifeTime(p) {
            //convert p sang dạng int
            this.fixedAsset.lifeTime = this.convertIntNumber(p)
        },
    },

    created() {
        var me = this;
        //gán giá trị prop cho data fixedAsset
        me.fixedAsset = me.fixedAssetSelected;
        //gán lại giá trị sang kiểu số cho các giá trị numeric
        me.valueCost = me.fixedAsset.cost;
        me.valueDepreciationValueYear = me.fixedAsset.depreciationValueYear;
        me.valueQuantity = me.fixedAsset.quantity;
        me.valueLifeTime = me.fixedAsset.lifeTime;
        me.valueDepreciationRate = me.fixedAsset.depreciationRate * 100;
        //Đặt giá trị mặc định cho ngày mua và ngày bắt đầu sử dụng
        me.fixedAsset.purchaseDate = new Date();
        me.fixedAsset.productionDate = new Date();

        //Lấy ra danh sách phòng ban
        axios.get("http://localhost:64168/api/v1/departments")
            .then(function (res) {
                me.departments = res.data;
            })
            .catch(function (res) {
                console.log(res);
            });

        //Lấy ra danh sách loại tài sản
        axios.get("http://localhost:64168/api/v1/fixedAssetCategories")
            .then(function (res) {
                me.fixedAssetCategories = res.data;
            })
            .catch(function (res) {
                console.log(res);
            })

        // Gán hàm dateOfBirth (Ngày sinh) = Hàm formatDate để xử lý dữ liệu ngày tháng
        me.fixedAsset.purchaseDate = me.formatDate(me.fixedAsset.purchaseDate);
        me.fixedAsset.productionDate = me.formatDate(me.fixedAsset.productionDate);
    },

    mounted() {
        var me = this;
        //Focus vào ô đầu tiên
        me.$refs.txtAssetCodeRef.focus();
    }
}
</script>
