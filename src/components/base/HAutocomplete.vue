<template>
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
</template>

<script>
    export default {
        data() {
            return {
                departments: [],
            }
        },

        methods: {
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
                //Autocomplete
                me.fixedAsset.departmentId = dep[me.arrowDepartmentsCounter].departmentId;
                me.fixedAsset.departmentCode = dep[me.arrowDepartmentsCounter].departmentCode;
                me.fixedAsset.departmentName = dep[me.arrowDepartmentsCounter].departmentName;
                //So sánh mảng couter với chiều dài của mảng phòng ban
                if (me.arrowDepartmentsCounter < me.departments.length - 1)
                    me.arrowDepartmentsCounter++;
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
                //Autocomplete
                me.fixedAsset.departmentId = dep[me.arrowDepartmentsCounter].departmentId;
                me.fixedAsset.departmentCode = dep[me.arrowDepartmentsCounter].departmentCode;
                me.fixedAsset.departmentName = dep[me.arrowDepartmentsCounter].departmentName;
                //So sánh mảng couter với chiều dài của mảng phòng ban
                if (me.arrowDepartmentsCounter > 0)
                    me.arrowDepartmentsCounter--;
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
        },

        created() {
            //Lấy ra danh sách phòng ban
            axios.get("http://localhost:64168/api/v1/departments")
            .then(function (res) {
                me.departments = res.data;
            })
            .catch(function (res) {
                console.log(res);
            });
        },
    }
</script>
