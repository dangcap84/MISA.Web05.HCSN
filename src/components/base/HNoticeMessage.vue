<template>
    <div id="popupMessage" class="popup-message">
                <div class="popup-message-layer"></div>
                <div class="popup-message-body">
                    <div class="popup-content-wrap">
                        <i class="fas fa-exclamation-triangle error-icon"></i>
                        <div id="txtErrorMessage" class="popup-message-content" >
                            <div v-for="error of noticeMessage" :key="error" class="popup-message-content-text">{{error}}</div> 
                        </div>
                    </div>
                    <div class="popup-footer">
                        <button class="main-button white-speial-rectangle-button" @click="showDetail">Chi tiết</button>
                        <button class="main-button rectangle-button" @click="closeNotice">Đồng ý</button>
                    </div>
                </div>
            </div>
</template>
<script>
    export default {
    name: "HNoticeMessage",
    props: {
        noticeMessage: {
            type: Array,
        },
    },
    data() {
        return {
            
        }
    },

    methods: {
        /**
         * Tắt modal
         * NDHoang(07/07/2022)
         */
        closeNotice() {
            this.$emit('closeNotice', false);
        },

        /**
         * Hiện chi tiết
         * NDHoang(07/07/2022)
         */
        showDetail() {
            this.$emit('showDetail', true);         
        }
    },

    }

    
</script>