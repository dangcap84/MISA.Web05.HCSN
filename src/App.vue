<template>
  <Navbar></Navbar>
  <Header></Header>
  <Content></Content>
</template>

<script>
//B1
import Header from "./components/layout/ConstHeader.vue"
import Navbar from "./components/layout/ConstNavbar.vue"
import Content from "./components/layout/ConstContent.vue"

export default {
  name: 'App',
  components: {
    Header,Navbar,Content
  }
}
</script>

<style>
@import url(../src/css/main.css);
</style>