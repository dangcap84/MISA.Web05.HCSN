<template>
<div id="popupMessage" class="popup-message">
    <div class="popup-message-layer"></div>
    <div class="popup-message-body">
        <div class="popup-content-wrap">
            <i class="fas fa-exclamation-triangle error-icon"></i>
            <div id="txtErrorMessage" class="popup-message-content">
                {{messageDelete}}
            </div>
        </div>
        <div class="popup-footer">
            <button class="main-button white-rectangle-button modal-button-right" @click="closeNotice">Không</button>
            <button class="main-button rectangle-button modal-button-right" @click="deleteFixedAsset">Xóa</button>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name: "HToastMessage",
    props: {
        messageDelete: {
            type: String,
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

        /**
         * Xóa đối tượng
         * NDHoang(07/07/2022)
         */
        deleteFixedAsset() {
            this.$emit('deleteFixedAsset')
        }
    },
}
</script>
