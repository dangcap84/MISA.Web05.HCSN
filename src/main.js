import { createApp } from 'vue'
import App from './App.vue'
// import axios from 'axios';
import HLoading from "@/components/base/HLoading.vue";
import HToastMessage from "@/components/base/HToastMessage.vue";
import HPostMessage from "@/components/base/HPostMessage.vue";
import HPutMessage from "@/components/base/HPutMessage.vue";
import HDeleteMessage from "@/components/base/HDeleteMessage.vue";
import HErrorMessage from "@/components/base/HErrorMessage.vue";
import HNoticeMessage from "@/components/base/HNoticeMessage.vue";
import HDetailMessage from "@/components/base/HDetailMessage.vue";
import '@imengyu/vue3-context-menu/lib/vue3-context-menu.css';
import ContextMenu from '@imengyu/vue3-context-menu';
import vClickOutside from "click-outside-vue3"
import router from '@/js/router.js';
import store from '@/js/store.js';

// axios.interceptors.response.use(undefined, function(error) {
//     if (error) {
//         const originalRequest = error.config;
//         if (error.response.status === 404 && !originalRequest._retry) {
//             originalRequest._retry = true;
//             store.dispatch("logout");
//             return router.push("/");
//         }
//     }
// });

// 5. Create and mount the root instance.
const app = createApp(App);
app.use(router).mount('#app');
app.use(ContextMenu);
app.use(vClickOutside);
app.use(store);
app.component("HLoading", HLoading);
app.component("HToastMessage", HToastMessage);
app.component("HPutMessage", HPutMessage);
app.component("HDeleteMessage", HDeleteMessage);
app.component("HPostMessage", HPostMessage);
app.component("HErrorMessage", HErrorMessage);
app.component("HNoticeMessage", HNoticeMessage);
app.component("HDetailMessage", HDetailMessage);