<template>
<div class="login-wrap">
    <div class="login-body">
        <div class="login-body-img"></div>
        <div class="login-body-input">
            <div class="logo-body-textwrap">
                <div class="login-input-logo"></div>
                <routerLink to="/" class="login-input-title">Đăng nhập để làm việc với <b>MISA QLTS</b></routerLink>
            </div>
            <form @submit.prevent="submit" action="" class="input-body-inputwrap">
                <input v-model="form.username" type="text" placeholder="Username, email hoặc số điện thoại" class="login-input-username">
                <input v-model="form.password" autocomplete="off" type="password" placeholder="Mật khẩu" class="login-input-password">
                <button type="submit" class="main-button login-input-btnlogin">Đăng nhập</button>
                <div class="login-input-forgetpassword">Quên mật khẩu?</div>
            </form>
        </div>
    </div>
</div>
<HErrorMessage v-if="isShowErrorNotice" @closeNotice="showErrorNotice" :errorMessage="errorMessage" />
</template>

<script>
import axios from 'axios';
import {
    mapActions
} from 'vuex';
import store from '../../js/store';
import resource from '@/js/resource';
//Set mặc định request lên serve kèm theo cookie
axios.defaults.withCredentials = true;

export default {
    name: 'LoginPage',
    components: {},

    data() {
        return {

            //Tạo biến chứa error message
            errorMessage: [],
            isShowErrorNotice: false,

            form: {
                username: '',
                password: '',
            },
        }
    },

    methods: {
        ...mapActions(["login"]),

        async submit() {
            const user = {
                username: this.form.username,
                password: this.form.password
            }
            await this.login(user);
            this.errorMessage = [];
            if (store.getters.isWrongPass == false) {
                this.$router.push("/fixedasset");
                this.isShowErrorNotice= false;
            } else {
                this.errorMessage.push(resource.account.error);
                this.isShowErrorNotice = true;
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
    }
}
</script>

<style>
@import url(../../css/layout/login.css);
</style>
