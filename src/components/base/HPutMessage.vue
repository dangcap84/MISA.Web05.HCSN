<template>
    <div id="popupMessage" class="popup-message">
                <div class="popup-message-layer"></div>
                <div class="popup-message-body">
                    <div class="popup-content-wrap">
                        <i class="fas fa-exclamation-triangle error-icon"></i>
                        <div id="txtErrorMessage" class="popup-message-content" >
                            {{errorMessage}}
                        </div>
                    </div>
                    <div class="popup-footer">
                        <button id="btnAccept" class="main-button white-rectangle-button" @click="closeNotice">Hủy</button>
                        <button id="btnAccept" class="main-button white-speial-rectangle-button" @click="closeModal">Không lưu</button>
                        <button id="btnAccept" class="main-button rectangle-button" @click="saveOnClick">Đồng ý</button>
                    </div>
                </div>
            </div>
</template>
<script>
    export default {
    name: "HToastMessage",
    data() {
        return {
            errorMessage: 'Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?',
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
        },

        saveOnClick() {
            this.$emit('saveOnClick')
        }
    },
    }
</script>