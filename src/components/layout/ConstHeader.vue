<template>
<div class="header">
    <div class="header-title">
        Danh sách tài sản
    </div>
    <div class="header-title-menu">
        <div class="title-menu-item1">Sở tài chính</div>
        <div class="title-menu-item2">
            <div class="title-menu-sub-item1">Năm</div>
            <div class="title-menu-sub-item2">2022</div>
            <div class="title-menu-sub-item3"></div>
        </div>
        <div class="title-menu-item3" title="Thông báo"></div>
        <div class="title-menu-item4" title="Lựa chọn"></div>
        <div class="title-menu-item5" title="Hỏi đáp"></div>
        <div class="title-menu-item6" title="Tài khoản" @click="showAccountOption">
            <div class="title-menu-item7"></div>
            <div class="account-option" v-click-outside="hideAccountOption"  v-if="isShowAccountOption" @click="logOut">Đăng xuất</div>
        </div>
    </div>
</div>
</template>

<style>
@import url(../../css/layout/header.css);
</style>

<script>
import {
    mapActions
} from 'vuex';
export default {
    data() {
        return {
            isShowAccountOption: false,
        }
    },  

    methods: {
        /**
         * Khai báo mapAction để sử dụng hàm logout của store
         * NDHoang(09/08/2022)
         */
        ...mapActions(["logout"]),

        /**
         * sử dụng hàm logout của store
         * NDHoang(09/08/2022)
         */
        async logOut() {
            await this.logout()
            this.$router.go();
        },

        /**
         * Hiện nút đăng xuất
         * NDHoang(09/08/2022)
         */
        showAccountOption(){
            this.isShowAccountOption = true;
        },

        /**
         * Ẩn nút đăng xuất
         * NDHoang(09/08/2022)
         */
        hideAccountOption(){
            this.isShowAccountOption = false;
        }
    }
}
</script>
