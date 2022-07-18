<template>
    <div id="popupMessage" class="popup-message">
                <div class="popup-message-layer"></div>
                <div class="popup-message-body">
                    <div class="popup-content-wrap">
                        <i class="fas fa-exclamation-triangle error-icon"></i>
                        <div id="txtErrorMessage" class="popup-message-content" >
                            Bạn có muốn hủy bỏ khai báo tài sản này?
                        </div>
                    </div>
                    <div class="popup-footer">
                        <button class="main-button white-rectangle-button modal-button-right" @click="closeNotice">Không</button>
                        <button class="main-button rectangle-button modal-button-right" @click="closeModal">Hủy bỏ</button>
                    </div>
                </div>
            </div>
</template>
<script>
    export default {
    name: "HToastMessage",
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

        closeModal() {
            this.$emit('closeModal')
        }
    },

    watch: {

    },

    created() {
        this.fixedAsset = this.fixedAssetSelected;
    }
    }

    
</script>