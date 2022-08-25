import FixedAssetTableVue from "@/view/fixedAsset/FixedAssetTable.vue";
import IncreaseWritingTableVue from "@/view/increaseasset/IncreaseWritingTable.vue";
import LoginPageVue from "@/view/login/LoginPage.vue";
import { createRouter, createWebHistory } from 'vue-router';
import store from './store';


const routes = [{
        path: "/",
        redirect: "/login",
        name: "Main",
    },
    {
        path: "/fixedasset",
        name: "Asset",
        component: FixedAssetTableVue,
        meta: { requiresAuth: true },
    },
    {
        path: "/increaseasset",
        name: "IncreaseAsset",
        component: IncreaseWritingTableVue,
        meta: { requiresAuth: true },
    },
    {
        path: "/login",
        name: "Login",
        component: LoginPageVue,
        meta: { requiresAuth: false },
    },

];

const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHistory(),
    routes, // short for `routes: routes`
});

/**
 * Mỗi khi chuyển route thì vue-router sẽ chạy hàm beforeEach ,
 * kiếm tra các tịnh trạng như isAuthentication để xem user đã đăng nhập hay chưa
 * nếu chưa thì không cho rời trang login
 * nếu rồi thì không cho chuyển trang tới trang login nữa trừ khi đăng xuất
 * NDHoang(08/08/2022)
 */
router.beforeEach(async(to, from, next) => {
    if (to.matched.some((record) => record.meta.requiresAuth)) {
        if (store.getters.isAuthentication) {
            next();
        } else next({ path: '/login' });
    } else {
        if (store.getters.isAuthentication) {
            next({ path: '/fixedasset' });
        } else next();
    }
});


export default router;