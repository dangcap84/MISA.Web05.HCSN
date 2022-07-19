import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHashHistory } from 'vue-router';
import FixedAssetTableVue from "@/view/fixedAsset/FixedAssetTable.vue";
import CustomerTableVue from "@/view/customer/CustomerTable.vue";
import HLoading from "@/components/base/HLoading.vue";
import HToastMessage from "@/components/base/HToastMessage.vue";
import HPostMessage from "@/components/base/HPostMessage.vue";
import HPutMessage from "@/components/base/HPutMessage.vue";
import HDeleteMessage from "@/components/base/HDeleteMessage.vue";
import HErrorMessage from "@/components/base/HErrorMessage.vue";
import '@imengyu/vue3-context-menu/lib/vue3-context-menu.css';
import ContextMenu from '@imengyu/vue3-context-menu';
import vClickOutside from "click-outside-vue3"


const misaroutes = [
    { path: '/', component: FixedAssetTableVue },
    { path: '/customer', component: CustomerTableVue },
]

// 3. Create the router instance and pass the `routes` option
// You can pass in additional options here, but let's
// keep it simple for now.
const router = createRouter({
    // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
    history: createWebHashHistory(),
    routes: misaroutes, // short for `routes: routes`
})


// 5. Create and mount the root instance.
const app = createApp(App);
app.use(router).mount('#app');
app.use(ContextMenu);
app.use(vClickOutside);
app.component("HLoading", HLoading);
app.component("HToastMessage", HToastMessage);
app.component("HPutMessage", HPutMessage);
app.component("HDeleteMessage", HDeleteMessage);
app.component("HPostMessage", HPostMessage);
app.component("HErrorMessage", HErrorMessage);