import axios from "axios";
import createPersistedState from "vuex-persistedstate";
import { createStore } from "vuex";
import resources from './resource.js';


const store = createStore({

    state() {
        return {
            user: null,
            wrongPass: false,
            Authentication: false,
        };
    },
    mutations: {
        /**
         * Set lại user
         * NDHoang(08/08/2022)
         */
        setUser(state, username) {
            state.user = username;
        },
        /**
         * Hàm đăng xuất, xóa user trên store
         * NDHoang(08/08/2022)
         */
        logout(state) {
            state.user = null;
        },
        /**
         * Set trạng thái có sai mật khẩu hay không
         * NDHoang(08/08/2022)
         */
        setWrongPass(state, isWrongPass) {
            state.wrongPass = isWrongPass;
        },

        /**
         * Set trạng thái đã xác thực hay chưa
         * NDHoang(08/08/2022)
         */
        setAuthentication(state, isAuthentication) {
            state.Authentication = isAuthentication;
        }
    },
    getters: {
        /**
         * Hàm kiểm tra authen hay chưa bằng cách so sánh user
         * NDHoang(08/08/2022)
         */
        isAuthentication(state) {
            return state.Authentication;
        },
        /**
         * Check và lấy ra xem mật khẩu có đang sai hay không
         * NDHoang(08/08/2022)
         */
        isWrongPass(state) {
            return state.wrongPass;
        }

    },
    actions: {
        /**
         * Hàm thực hiện login ở vuex với user được commit lên
         * NDHoang(08/08/2022)
         */
        async login({ commit }, user) {
            // var me = this;
            await axios.post(resources.url.host + "/api/v1/Users", user, {
                    withCredentials: true,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then(async function(res) {
                    console.log(res.data);
                    if (res.data != -1) {
                        await commit("setUser", user.username);
                        await commit("setAuthentication", true);
                        await commit("setWrongPass", false);
                    } else {
                        await commit("setAuthentication", false);
                        await commit("setWrongPass", true);
                    }

                })
        },
        /**
         * Hàm đăng xuất, call api xóa cookie
         * NDHoang(08/08/2022)
         */
        async logout({ commit }) {
            await commit("logout");
            await axios.get(resources.url.host + "/api/v1/Users")
            await commit("setAuthentication", false);
        },
    },

    plugins: [createPersistedState()],


});

export default store;