<template>
    <div id="expandMenu" class="main-menu" :class="{'menu_style': expand}">
            <div class="logo-wrap">
                <div title="click để mở rộng" id="expandMenu" class="logo"></div>
                <div class="logo-item-text">MISA QLTS</div>
            </div>
            <div class="navigator-menu" @click="expandMenu">
                <div title="Tổng quan" class="navigator-item-wrap">
                    <div class="navigator-item1 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Tổng quan</div>
                    <div class="icon" style="visibility: hidden;"></div>
                </div>
                <div title="Tài sản" class="navigator-item-wrap navigator-item-selected" :class="{'navigator-subitem-open': isShowSubmenu}" @click="showSubmenu" @dblclick="hideSubmenu">
                    <div class="navigator-item2 menu-item_general-column"></div>
                    <div class="navigato-item-text">Tài sản</div>
                    <div class="icon icon-special_style"></div>
                </div>
                <div v-if="isShowSubmenu" class="navigator-subitem-wrap">
                        <router-link to="/increaseasset" class="navigator-subitem">Ghi tăng</router-link>
                        <router-link to="/fixedasset" class="navigator-subitem">Thay đổi thông tin</router-link>
                        <div class="navigator-subitem">Đánh giá lại</div>
                        <div class="navigator-subitem">Tính hao mòn</div>
                        <div class="navigator-subitem">Điều chuyển tài sản</div>
                        <div class="navigator-subitem">Ghi giảm</div>
                        <div class="navigator-subitem">Kiểm kê</div>
                        <div class="navigator-subitem">Khác</div>
                    </div>
                <div title="Tài sản Hao trừ / Đặc biệt" class="navigator-item-wrap" style="margin-top:0">
                    <div class="navigator-item3 menu-item_general-column">
                    </div>
                    <div class="navigato-item-text">Tài sản HT-ĐB</div>
                    <div class="icon"></div>
                </div>
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
        expandMenu(){
            this.expand = false;
        },

        unexpandMenuOnclick(){
            this.expand = true;
            this.isShowSubmenu = false;
        },

        //Hiện sub menu
        showSubmenu() {
            if(this.isShowSubmenu){
                this.isShowSubmenu = false;
            }else{
                this.isShowSubmenu = true;
            }
        },

        //Ẩn Submenu
        hideSubmenu() {
            this.isShowSubmenu = false;
        }
    },

    data() {
        return {
            isShowSubmenu: false,
            expand: true,
            employees: []
        }
    },
}
</script>