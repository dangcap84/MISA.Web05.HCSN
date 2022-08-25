<template>
<div id="dbEpl" class="modal selected-modal-style" @keyup.esc="closeModalOnclick">
    <div class="modal-layer"></div>
    <div class="modal-body">
        <div class="modal-body-title">
            <div class="modal-body-title_text">{{modalTitle}}</div>
            <div id="closeBtn" class="close-icon" @click="hideSelected"></div>
        </div>
        <div class="body-input_right validate-text">
                    <div class="body-input-text">Bộ phận sử dụng
                    </div>
                    <div class="input-icon">
                        <input v-model="departmentName" type="text" class="input input-modal_style-right input-disable" disabled>
                    </div>
                </div>
        <div class="body-sub-title">Nguyên giá</div>
        <div class="body-input-row">
            <div class="body-input_left validate-text" style="margin-right: 270px">
                <div class="body-input-text">Nguyồn hình thành
                </div>
            </div>
            <div class="body-input_left validate-text">
                    <div class="body-input-text">Giá trị
                    </div>
            </div>
        </div>
        <div class="body-input-row" v-for="(source,indexs) in moneySources" :key="indexs">
                <div class="body-input_left validate-text">
                    <div class="input-icon">
                        <div class="combobox combobox-modal-style">
                            <div class="combobox-selected " :class="{'not-valid': isValid,'onfocus': isShowListDepartments[indexs] == indexs}">
                                <input maxlength="50" ref="txtDepartmentCodeRef" 
                                @keyup.enter="selectListByEnter"
                                @keydown.tab="tabOutCombobox"
                                @keydown.up="arrowListUp($event,moneyNameSources,indexs)" 
                                @keydown.down="arrowListDown(moneyNameSources,indexs)"
                                autocomplete="off" type="text" 
                                v-model="moneySources[indexs].nameSource"
                                class="selected-item" 
                                placeholder="Chọn nguồn hình thành" 
                                @focusin="departmentsComboboxOnFocus(indexs)">
                                <div class="input-icon3"></div>
                                <div ref="comboboxDepartments" class="combobox-list" :class="{'visible': isShowListDepartments[indexs] == indexs}">
                                    <div ref="optionsDepartments" v-for="(moneyNameSource,index) in moneyNameSources" :key="index" :class="{'ishover': index==arrowLicenseCounter}" class="combobox-items" @click="listDepartmentsOnClick(moneyNameSource,indexs)">
                                        <div class="">{{moneyNameSource}}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div :class="{'visible': isValid}" id="txtErrordepartmentCode" class="input-validate-error">Nguồn chi phí sử dụng không được trùng</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="input-icon">
                        <input v-model="moneySources[indexs].cost" v-money="money" maxlength="14" type="text" class="input modal-input_style-left input-quality_style" :class="{'not-valid': isNull}">
                    </div>
                    <div :class="{'visible': isNull}" class="input-validate-error">Giá trị không được để trống</div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="input-icon-wrap">
                        <div class="incon-add-row" title="Thêm nguồn chi phí" @click="addBudget()">
    
                        </div>
                        <div v-if="moneySources.length > 1" class="incon-remove-row" title="Bỏ nguồn chi phí" @click="removeBudget(indexs)">
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="body-input-row row-footer">
                <div class="body-input_left validate-text">
                    <div class="input-icon">
                        <input maxlength="14" type="text" class="input modal-input_style-left" value="Tổng" disabled>
                    </div>
                </div>
                <div class="body-input_left validate-text">
                    <div class="input-icon">
                        <input maxlength="14" type="text" class="input modal-input_style-left input-quality_style" :value="totalCost" disabled>
                    </div>
                </div>
            </div>
        <div class="modal-footer">
            <button id="btnSave" class="main-button rectangle-button modal-button-right" @click="saveOnClick">Lưu</button>
            <button id="btnCancle" class="main-button white-rectangle-button modal-button-left" @click="hideSelected">Hủy</button>
        </div>
    </div>
</div>
</template>

<style lang="css">
/* Write your own CSS for pagination */
@import url(../../css/base/datepicker.css);
</style>

<script>
import resources from '../../js/resource.js';
//Sử dụng thư viên V-money
import {
    VMoney
} from 'v-money';

import axios from 'axios';

export default {
    name: "EmployeeDetail",
    emits: ["closeModal", "resetTable", "showSuccessMessage","closeSelected","getJsonDetail"],
    directives: {
        money: VMoney,
    },

    data() {
        return {
            //Tạo đối tượng money của v-money
            money: {
                decimal: ',',
                thousands: resources.currency,
                precision: 0,
            },

            //Tạo biến lưu tên phòng ban
            departmentName: '',
            fixedAssetName: '',

            //Đối tượng nguồn tài sản
            source: {},

            //Tạo biến lưu giá trị hiển thị
            isShow: true,
            isHide: false,

            //Tạo title Modal
            modalTitle: '',

            //Ẩn combobox list
            isShowListDepartments: [],

            //Tạo mảng đối tượng
            licenses: [],

            //Tạo biến arrow counter
            arrowLicenseCounter: 0,

            //Khai báo 
            valueCost: 0,

            //Khai báo biến validate
            isValid: false,
            isNull: false,
            
             // Mảng check trùng
            moneyNameSources:[
                "Ngân sách xã",
                "Ngân sách tỉnh",
                "Ngân sách huyện",
                "Ngân sách nhà nước",
            ],
            // Mảng to gồm cả tên cả số tiền
            moneySources:[
                {
                    nameSource: "Ngân sách xã",
                    cost: 0
                },
            ],

        }

    },

    /**
     * Khai báo prop sử dụng bên ngoài
     */
    props: {
        indexRowDetail: {
            type: Number,
        },

        fixedAssetSelected: {
            type: Object,
        },

        licenseDetailIdSelected: {
            type: String,
        },

        jsonDetailSelected: {
            type: Array,
        },

        licenseSelected: {
            type: Object
        },

        editMode: {
            type: Number,
            default: 0, // thêm mới
            require: true
        },
    },

    methods: {
        hideSelected(){
            this.$emit("closeSelected",false);
        },


         /**
         * sự kiện click nót thêm
         * NDHoang(22/07/2022)
         */
        saveOnClick() {
            try {
                var test = '';
                //Validate dữ liệu
                this.isValid = false;
                this.isNull = false;
                //check trùng
                for(var i = 0; i < this.moneySources.length-1; i++){
                for(var j = i + 1; j < this.moneySources.length; j ++){
                    if(this.moneySources[i].nameSource == this.moneySources[j].nameSource){
                        this.isValid = true;
                        break;
                    }
                    else this.isValid = false;
                }
                //check null
                for(const money of this.moneySources){
                    if(money.cost == 0){
                        this.isNull = true
                    }else {
                        this.isNull = false
                    }
                }
                }
                if(!this.isValid && !this.isNull){
                    test = JSON.stringify(this.moneySources);
                    this.$emit("getJsonDetail",test,this.indexRowDetail);
                    this.$emit("closeSelected",false);
                }
            } catch (error) {
                console.log(error);
            }
            
        },

        /**
         * Thêm dòng
         * NDHoang(22/07/2022)
         */

        addBudget(){
            this.moneySources.push({ nameSource: "",
                    cost: 0});
        },

        /**
         * Xóa dòng
         * NDHoang(22/07/2022)
         */

        removeBudget(index){
            if(index > -1){
                this.moneySources.splice(index,1);
            }
        },
        
        /**
         * Autocomplete
         * NDHoang(22/07/2022)
         */

        //Department autoComplete
        autoCompleteDepartment(name,indexs){
            var me = this;
            //Gán giá trị
            me.moneySources[indexs].nameSource = name;
        },

        /**
         * Sử dụng phím mũi tên
         * NDHoang(22/07/2022)
         */
        // Dùng phím mũi tên xuống để di chuyển lựa chọn
        arrowListDown(object,indexs) {
            try {
                var me = this;
                    //Hiện list danh sách phòng ban
                    me.isShowListDepartments[indexs] = indexs;
                    //So sánh mảng couter với chiều dài của mảng phòng ban
                    if (me.arrowLicenseCounter < object.length - 1)
                        me.arrowLicenseCounter++;
                    //Autocomplete
                    me.autoCompleteDepartment(object[me.arrowLicenseCounter],indexs);
                    me.scrollDepartmentsTo();
                
            } catch (error) {
                console.log(error);
            }
        },
        
        // Dùng phím mũi tên lên để di chuyển lựa chọn
        arrowListUp(e,object,indexs) {
            try {
                e.preventDefault();
                var me = this;
                    //Hiện list danh sách phòng ban
                    me.isShowListDepartments[indexs] = indexs;
                    //So sánh mảng couter với chiều dài của mảng phòng ban
                    if (me.arrowLicenseCounter > 0)
                        me.arrowLicenseCounter--;
                    //Autocomplete
                    me.autoCompleteDepartment(object[me.arrowLicenseCounter],indexs);
                    me.scrollDepartmentsTo();
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Cuộn theo phím mũi tên
         * NDHoang(22/07/2022)
         */
        
        scrollDepartmentsTo() {
            try {
                //Lấy chiều cao của list
                const itemHeight = this.$refs.optionsDepartments[this.arrowLicenseCounter].clientHeight;
                //Cuộn
                this.$refs.comboboxDepartments.scrollTop = itemHeight * this.arrowLicenseCounter - this.$refs.comboboxDepartments.clientHeight / 2;
            }
            catch (error) {
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
                me.isShowListDepartments = [];
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
                me.isShowListDepartments = [];
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
                if(!me.fixedAsset.fixedAssetCategoryCode){
                    me.fixedAssetCategoriesFilter = me.fixedAssetCategories
                } else {
                    me.fixedAssetCategoriesFilter = me.fixedAssetCategories.filter((Category) => {
                    return Category.fixedAssetCategoryCode.toLowerCase().match(me.fixedAsset.fixedAssetCategoryCode.toLowerCase());
                    });
                }
                return me.fixedAssetCategoriesFilter;
            } catch (error) {
                console.log(error);
            }
        },

        //Focus on combobox Bộ phận sử dụng
        departmentsComboboxOnFocus(indexs) {
            try {
                var me = this;
                //Ẩn list
                me.isShowListDepartments = [];
                //Hiện list phòng ban chỉ định
                me.isShowListDepartments[indexs] = indexs;
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện combobox item on click
         * NDHoang (23/06/2022)
         */
        //Click vào combobox bộ phận sử dụng items
        listDepartmentsOnClick(moneyNameSource,indexs) {
            try {
                var me = this;
                //Autocomplete
                me.autoCompleteDepartment(moneyNameSource,indexs);
                //Ẩn list
                me.isShowListDepartments = [];
            } catch (error) {
                console.log(error);
            }
        },

        convertIntNumber(num) {
            //Ép kiểu cho num
            num = parseInt(String(num).replaceAll(/\D+/g, ''))
            return num;
        },

        /**
         * Format money
         * NDHoang (04/07/2022)
         */
        formatMoney(money) {
            try {
                //Làm tròn dữ liệu đầu vào
                money = String(Math.floor(money))
                return money.replace(/\B(?=(\d{3})+(?!\d))/g,resources.currency);
            } catch (error) {
                console.log(error);
            }
        },
    },

    computed: {
        totalCost: function(){
            var total = this.moneySources.reduce((item1,item2) => {
                return item1 + this.convertIntNumber(item2.cost);
            },0);
            return this.formatMoney(total);
        }
    },

    created() {
        var me = this;
        if(me.licenseDetailIdSelected){
            axios.get(resources.url.host + `/api/v1/LicenseDetails/GetMoneySource/${me.licenseDetailIdSelected}`)
            .then(function (res) {
                //Gán lại giá trị cho đối tượng
                me.departmentName = res.data.DepartmentName;
                me.fixedAssetName = res.data.FixedAssetName;
                me.modalTitle = resources.modalTitle.updateTitle + `${me.fixedAssetName}`;
                if(res.data.DetailJson ) me.moneySources = JSON.parse(res.data.DetailJson);
            })
        }else {
            me.departmentName = me.fixedAssetSelected.departmentName;
            me.fixedAssetName = me.fixedAssetSelected.fixedAssetName;
            me.modalTitle = resources.modalTitle.updateTitle + `${me.fixedAssetName}`;
        }
        //Gán giá trị cho modal
        
    }
}
</script>