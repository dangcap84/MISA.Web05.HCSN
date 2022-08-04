<template>
    <div class="login-wrap">
        <div class="login-body">
            <div class="login-body-img"></div>
            <div class="login-body-input">
                <div class="logo-body-textwrap">
                    <div class="login-input-logo"></div>
                    <div class="login-input-title">Đăng nhập để làm việc với <b>MISA QLTS</b></div>
                </div>
                <div class="input-body-inputwrap">
                    <input type="text" placeholder="Username, email hoặc số điện thoại" class="login-input-username">
                    <input type="password" placeholder="Mật khẩu" class="login-input-password">
                    <button type="submit" class="main-button login-input-btnlogin">Đăng nhập</button>
                    <div class="login-input-forgetpassword">Quên mật khẩu?</div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  name: 'LoginPage',
  components: {
  }
}
</script>

<style>
@import url(../../css/layout/login.css);
</style>