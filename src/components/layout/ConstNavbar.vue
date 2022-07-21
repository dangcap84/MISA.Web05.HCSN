<template>
    <div id="expandMenu" class="main-menu" :class="{'menu_style': expand}">
            <div class="logo-wrap">
                <div title="click để mở rộng" id="logoOnClick" class="logo" @click="logoOnClick"></div>
                <div class="logo-item-text">MISA QLTS</div>
            </div>
            <div class="navigator-menu">
                <div title="Tổng quan" class="navigator-item-wrap">
                    <div class="navigator-item1 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Tổng quan</div>
                    <div class="icon" style="visibility: hidden;"></div>
                </div>
                <router-link to="/" title="Tài sản" class="navigator-item-wrap navigator-item-selected">
                    <div class="navigator-item2 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Tài sản</div>
                    <div class="icon icon-special_style"></div>
                </router-link>
                <router-link to="/customer" title="Tài sản Hao trừ / Đặc biệt" class="navigator-item-wrap" style="margin-top:0">
                    <div class="navigator-item3 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Tài sản HT-ĐB</div>
                    <div class="icon"></div>
                </router-link>
                <div class="navigator-item-wrap" title="Công cụ dụng cụ">
                    <div class="navigator-item4 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Công cụ dụng cụ</div>
                    <div class="icon"></div>
                </div>
                <div class="navigator-item-wrap" title="Danh mục">
                    <div class="navigator-item5 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Danh mục</div>
                    <div class="icon" style="visibility: hidden;"></div>
                </div>
                <div class="navigator-item-wrap" title="Tra cứu">
                    <div class="navigator-item6 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Tra cứu</div>
                    <div class="icon"></div>
                </div>
                <div class="navigator-item-wrap" title="Báo cáo">
                    <div class="navigator-item7 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Báo cáo</div>
                    <div class="icon"></div>
                </div>
            </div>
            <div id="unExpandMenu" title="Nhấn để thu gọn" class="menu-footer" @click="unexpandMenuOnclick">
                <div class="footer-icon"></div>
            </div>
        </div>
</template>
<style>
@import url(../../css/layout/navigate.css);
</style>
<script>
export default {
    name: "EmployeeTable",
    components: {

    },
    methods: {
        //mở rộng menu
        logoOnClick(){
            this.expand = false;
        },

        unexpandMenuOnclick(){
            this.expand = true;
        }
    },

    data() {
        return {
            expand: true,
            employees: []
        }
    },
}
</script>