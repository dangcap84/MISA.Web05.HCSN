<template>
<div id="toast-success" class="toast">
    <div class="toast-icon">
        <i class="fas fa-check-circle"></i>
    </div>
    <div id="txtAction" class="toast-body">
        {{successActionMessage}}
    </div>
</div>
</template>
<script>
    export default {
    name: "HToastMessage",
    props: {
        editMode: {
            type: Number,
            default: 0, // thêm mới
            require: true
        }
    },
    data() {
        return {
            successActionMessage: '',
        }
    },

    method: {
        
    },

    mounted() {
        this.successActionMessage = "Lưu dữ liệu thành công";
    }
    }


    
    
</script>
