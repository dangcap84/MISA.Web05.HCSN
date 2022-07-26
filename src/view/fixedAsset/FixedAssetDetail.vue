<template>
<div id="dbEpl" class="modal" @keyup.esc="closeModalOnclick">
    <div class="modal-layer"></div>
    <div class="modal-body">
        <div class="modal-body-title">
            <div class="modal-body-title_text">{{modalTitle}}</div>
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
                        <input ref="txtAssetNameRef" v-model="fixedAsset.fixedAssetName" type="text" class="input input-modal_style-right" placeholder="Nhập tên tài sản" :class="{'not-valid': inValid.validFixedAssetName}">
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
                        <div class="combobox combobox-modal-style">
                            <div class="combobox-selected " :class="{'not-valid': inValid.validDepartmentCode,'onfocus': isShowListDepartments}">
                                <input ref="txtDepartmentCodeRef" v-click-outside="onClickOutsideDepartmentsCombobox" @keyup.enter="selectDepartmentsByEnter(departments)" @keydown.up="arrowDepartmentsUp(departments)" @keydown.down="arrowDepartmentsDown(departments)" autocomplete="off" type="text" v-model="fixedAsset.departmentCode" class="selected-item" placeholder="Chọn mã bộ phận sử dụng" @focusin="departmentsComboboxOnFocus">
                                <div class="input-icon3"></div>
                                <div ref="comboboxDepartments" class="combobox-list" :class="{'visible': isShowListDepartments}">
                                    <div ref="optionsDepartments" v-for="(dep,index) in departments" :key="dep.departmentId" :class="{'ishover': index==arrowDepartmentsCounter}" class="combobox-items" :value="dep.departmentId" @click="listDepartmentsOnClick(dep)">
                                        <div class="">{{dep.departmentCode}}</div>
                                        <div class="tabletext-wrap" style="width: 130px">{{dep.departmentName}}</div>
                                    </div>
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
                                <input ref="txtAssetCategoryRef" v-click-outside="onClickOutsideCategoriesCombobox" id="cbxTypeOf" @keyup.enter="selectListCategoriesByEnter(fixedAssetCategories)" @keydown.tab="tabOutCategoriesCombobox" @keydown.up="arrowListCategoriesUp(fixedAssetCategories)" @keydown.down="arrowListCategoriesDown(fixedAssetCategories)" autocomplete="off" type="text" v-model="fixedAsset.fixedAssetCategoryCode" class="selected-item" placeholder="Chọn mã loại tài sản" @focusin="fixedAssetCategoriesComboboxOnFocus">
                                <div class="input-icon3"></div>
                                <div ref="comboboxListCategories" class="combobox-list" :class="{'visible': isShowListCategoriesAssets}">
                                    <div ref="optionsListCategories" v-for="(cat,index) in fixedAssetCategories" :key="cat.fixedAssetCategoryId" :class="{'ishover': index==arrowCategoriesCounter}" class="combobox-items" :value="cat.fixedAssetCategoryId" @click="listfixedAssetCategoriesOnClick(cat)">
                                        <div class="">{{cat.fixedAssetCategoryCode}}</div>
                                        <div class="tabletext-wrap" style="width: 130px">{{cat.fixedAssetCategoryName}}</div>
                                    </div>
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
                    <div class="body-input-text">Tỉ lệ hao mòn (%)
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueDepreciationRate" v-money="float" type="text" class="input modal-input_style-left input-quality_style" :class="{'not-valid': inValid.validDepreciationRate}">
                    </div>
                    <div :class="{'visible': inValid.validDepreciationRate}" class="input-validate-error">Tỉ lệ hao mòn(%) không được để trống</div>
                </div>
            </div>
            <div class="body-input-row">
                <div class="body-input_left validate-text">
                    <div class="body-input-text">Ngày mua
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <Datepicker format="dd/MM/yyyy" arrowNavigation locale="vi" placeholder="VD:01/01/2022" textInput cancelText="" selectText="Chọn" v-model="fixedAsset.purchaseDate" :class="{'not-valid': inValid.validPurchaseDate}">
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
                        <Datepicker format="dd/MM/yyyy" arrowNavigation locale="vi" placeholder="VD:01/01/2022" cancelText="" selectText="Chọn" v-model="fixedAsset.productionDate" :class="{'not-valid': inValid.validProductionDate}">
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
                    <div class="body-input-text">Số năm sử dụng
                        <div class="red-tag"></div>
                    </div>
                    <div class="input-icon">
                        <input v-model="valueLifeTime" v-money="money" type="text" class="input modal-input_style-left input-quality_style modal-input_style-special" :class="{'not-valid': inValid.validLifeTime}">
                        <div class="input-icon4"></div>
                    </div>
                    <div :class="{'visible': inValid.validLifeTime}" id="txtErrorTimeOfUsing" class="input-validate-error">Số năm sử dụng không được để trống</div>
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
            //Tạo biến lưu giá trị giả của tỉ lệ
            fakeValue:0,

            //Tạo title Modal
            modalTitle: '',

            //tạo đối tượng tài sản
            fixedAsset: {},

            //tạo mảng phòng ban
            departments: [],

            //tạo mảng loại tài sản
            fixedAssetCategories: [],

            //Đặt mặc định ẩn list
            isShowListDepartments: false,
            isShowListCategoriesAssets: false,

            //Đặt mặc định ẩn cảnh báo
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
            errorMessage: [],

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

        //Category autoComplete
        autoCompleteCategory(id,code,name){
            var me = this;
            me.fixedAsset.fixedAssetCategoryId = id;
            me.fixedAsset.fixedAssetCategoryCode = code;
            me.fixedAsset.fixedAssetCategoryName = name;
        },

        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowListCategoriesDown(cat) {
            try {
                var me = this
                //Hiện list danh sách loại tài sản
                me.isShowListCategoriesAssets = me.isShow;
                //So sánh mảng couter với chiều dài của mảng loại tài sản
                if (me.arrowCategoriesCounter < me.fixedAssetCategories.length - 1)
                    me.arrowCategoriesCounter++;
                //Autocomplete
                me.autoCompleteCategory(cat[me.arrowCategoriesCounter].fixedAssetCategoryId,cat[me.arrowCategoriesCounter].fixedAssetCategoryCode,cat[me.arrowCategoriesCounter].fixedAssetCategoryName);
                me.scrollListCategoriesTo()
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
                if (me.arrowCategoriesCounter >= 0)
                    me.arrowCategoriesCounter--;
                //autocomplete
                me.autoCompleteCategory(cat[me.arrowCategoriesCounter].fixedAssetCategoryId,cat[me.arrowCategoriesCounter].fixedAssetCategoryCode,cat[me.arrowCategoriesCounter].fixedAssetCategoryName);
                me.scrollListCategoriesTo()
            } catch (error) {
                console.log(error);
            }
        },

        //ScrollTo
        scrollListCategoriesTo() {
            const itemHeight = this.$refs.optionsListCategories[this.arrowCategoriesCounter].clientHeight;
            this.$refs.comboboxListCategories.scrollTop = itemHeight * this.arrowCategoriesCounter - this.$refs.comboboxListCategories.clientHeight / 2;
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

        //Sự kiện tab ra ngoài combobox
        tabOutCategoriesCombobox() {
            try {
                var me = this;
                me.isShowListDepartments = me.isHide;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        //Sự kiên click ra ngoài combobox
        onClickOutsideCategoriesCombobox() {
            try {
                var me = this;
                me.isShowListCategoriesAssets = me.isHide;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện của các phím sử dụng trong combobox department
         * NDHoang(22/06/2022)
         */
        
        //Department autoComplete
        autoCompleteDepartment(id,code,name){
            var me = this;
            me.fixedAsset.departmentId = id;
            me.fixedAsset.departmentCode = code;
            me.fixedAsset.departmentName = name;
        },

        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowDepartmentsDown(dep) {
            try {
                var me = this;
                //Hiện list danh sách phòng ban
                me.isShowListDepartments = me.isShow;
                //So sánh mảng couter với chiều dài của mảng phòng ban
                if (me.arrowDepartmentsCounter < me.departments.length - 1)
                    me.arrowDepartmentsCounter++;
                //Autocomplete
                me.autoCompleteDepartment(dep[me.arrowDepartmentsCounter].departmentId,dep[me.arrowDepartmentsCounter].departmentCode,dep[me.arrowDepartmentsCounter].departmentName);
                me.scrollDepartmentsTo();
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
                me.autoCompleteDepartment(dep[me.arrowDepartmentsCounter].departmentId,dep[me.arrowDepartmentsCounter].departmentCode,dep[me.arrowDepartmentsCounter].departmentName);
                me.scrollDepartmentsTo();
            } catch (error) {
                console.log(error);
            }
        },

        //Scroll to
        scrollDepartmentsTo() {
            const itemHeight = this.$refs.optionsDepartments[this.arrowDepartmentsCounter].clientHeight;
            this.$refs.comboboxDepartments.scrollTop = itemHeight * this.arrowDepartmentsCounter - this.$refs.comboboxDepartments.clientHeight / 2;
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

        //Sự kiên click ra ngoài combobox
        onClickOutsideDepartmentsCombobox() {
            try {
                var me = this;
                me.isShowListDepartments = me.isHide;
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
                //Autocomplete
                me.autoCompleteDepartment(dep.departmentId,dep.departmentCode,dep.departmentName);
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
                me.autoCompleteCategory(cat.fixedAssetCategoryId,cat.fixedAssetCategoryCode,cat.fixedAssetCategoryName);
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
                                        me.errorMessage.push("Mã tài sản đã tồn tại trong hệ thống");
                                        console.log(error.response.data);
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
                            axios.put(`http://localhost:64168/api/v1/fixedAssets`, newAsset)
                                .catch(function (error) {
                                    if (error.response) {
                                        //Hiển thị popup lỗi
                                        me.errorMessage.push("Mã nhân viên đã tồn tại trong hệ thống");
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
            try {
                var me = this;
                me.isValid = true;
                //Check mã loại tài sản
                if (!me.fixedAsset.fixedAssetCategoryCode) {
                    me.isValid = false;
                    me.inValid.validFixedAssetCategoryCode = true;
                } else {
                    me.inValid.validFixedAssetCategoryCode = false;
                }
                //Check mã bộ phận sử dụng
                if (!me.fixedAsset.departmentCode) {
                    me.isValid = false;
                    me.inValid.validDepartmentCode = true;
                } else {
                    me.inValid.validDepartmentCode = false;
                }
                //Check tên tài sản
                if (!me.fixedAsset.fixedAssetName) {
                    me.isValid = false;
                    me.inValid.validFixedAssetName = true;
                    me.$refs.txtAssetNameRef.focus();
                } else {
                    me.inValid.validFixedAssetName = false;
                }
                //Check mã tài sản
                if (!me.fixedAsset.fixedAssetCode) {
                    me.isValid = false;
                    me.inValid.validFixedAssetCode = true;
                    me.$refs.txtAssetCodeRef.focus();
                } else {
                    me.inValid.validFixedAssetCode = false;
                }
                //Check số lượng
                if (!me.valueQuantity) {
                    me.isValid = false;
                    me.inValid.validQuantity = true;
                } else {
                    me.inValid.validQuantity = false;
                }
                //Check nguyên giá
                if (!me.valueCost) {
                    me.isValid = false;
                    me.inValid.validCost = true;
                } else {
                    me.inValid.validCost = false;
                }
                //Số năm sử dụng
                if (!me.valueLifeTime) {
                    me.isValid = false;
                    me.inValid.validLifeTime = true;
                } else {
                    me.inValid.validLifeTime = false;
                }
                //Check tỉ lệ hao mòn
                if (!me.valueDepreciationRate) {
                    me.isValid = false;
                    me.inValid.validDepreciationRate = true;
                } else {
                    me.inValid.validDepreciationRate = false;
                }
                //Check giá trị hao mòn 
                if (!me.valueDepreciationValueYear) {
                    me.isValid = false;
                    me.inValid.validDepreciationValueYear = true;
                } else {
                    me.inValid.validDepreciationValueYear = false;
                }
                //Check ngày mua
                if (!me.fixedAsset.purchaseDate) {
                    me.isValid = false;
                    me.inValid.validPurchaseDate = true;
                } else {
                    me.inValid.validPurchaseDate = false;
                }
                //Check ngày sử dụng
                if (!me.fixedAsset.productionDate) {
                    me.isValid = false;
                    me.inValid.validProductionDate = true;
                } else {
                    me.inValid.validProductionDate = false;
                }
                if (!me.isValid) {
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
            if (value >= 1) {
                this.fakeValue = value / 100;
            }
            // So sánh giá trị với max
            this.fixedAsset.depreciationRate = this.fakeValue.toFixed(2);
        },

        valueCost(p) {
            //convert p sang dạng int
            this.fixedAsset.cost = this.convertIntNumber(p);
            //Công thức tính giá trị hao mòn năm nguyên giá nhân với tỉ lệ hao mòn
            if(this.fixedAsset.depreciationRate > 1)
                this.fixedAsset.depreciationRate = this.fixedAsset.depreciationRate/100;
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
        //gán giá trị tài sản được chọn cho cho fixedAsset
        me.fixedAsset = me.fixedAssetSelected;
        //Xét trạng thái modal tương ứng
        if (me.editMode == me.insertMode) {
            me.modalTitle = "Thêm tài sản";
            //Đặt giá trị mặc định cho ngày mua và ngày bắt đầu sử dụng
            me.fixedAsset.purchaseDate = new Date();
            me.fixedAsset.productionDate = new Date();
        } else me.modalTitle = "Sửa tài sản";

        //gán lại giá trị sang kiểu số cho các giá trị numeric
        me.valueCost = me.fixedAsset.cost;
        me.valueQuantity = me.fixedAsset.quantity;
        me.valueLifeTime = me.fixedAsset.lifeTime;
        me.valueDepreciationRate = me.fixedAsset.depreciationRate;
        if(me.valueDepreciationRate < 1){
            me.valueDepreciationRate = (me.valueDepreciationRate*100).toFixed(2);
        }

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
    },

    mounted() {
        var me = this;
        //Focus vào ô đầu tiên
        me.$refs.txtAssetCodeRef.focus();
    }
}
</script>
