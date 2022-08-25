<template>
    <div id="popupMessage" class="popup-message">
                <div class="popup-message-layer"></div>
                <div class="popup-message-body">
                    <div class="popup-content-wrap">
                        <i class="fas fa-exclamation-triangle error-icon"></i>
                        <div class="popup-message-content popup-message-detail-style" >
                            <div v-for="detail of detailMessage" :key="detail" class="popup-message-content-text">{{detail}}</div> 
                        </div>
                    </div>
                    <div class="popup-footer">
                        <button class="main-button rectangle-button" @click="closeNotice">Đồng ý</button>
                    </div>
                </div>
            </div>
</template>
<script>
    export default {
    name: "HErrorMessage",
    props: {
        detailMessage: {
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
            this.$emit('closeNotice', false)
        },
    },

    }

    
</script>