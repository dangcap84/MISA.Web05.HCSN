var resource = {
    url: {
        host: "http://localhost:64168",
    },

    modalTitle: {
        insertTitle: "Thêm tài sản",
        updateTitle: "Sửa tài sản"
    },

    warning: {
        fixedAssetCodeDuplicateValue: "Mã tài sản đã tồn tại trong hệ thống",
        fixedAssetNameNullValue: "Tên tài sản không được để trống",
        departmentCodeNullValue: "Mã bộ phận không được để trống",
        fixedAssetCategoryCodeNullValue: "Mã loại tài sản không được để trống",
        quantityNullValue: "Số lượng không được để trống",
        costNullValue: "Nguyên giá không được để trống",
        depreciationRateNullValue: "Tỉ lệ hao mòn không được để trống",
        productionDateNullValue: "Ngày bắt đầu sử dụng không được để trống",
        purchaseDateNullValue: "Ngày mua không được để trống",
        lifeTimeNullValue: "Số năm sử dụng không được để trống",
        depreciationValueYearNullValue: "Giá trị hao mòn năm không được để trống",
    },

    import: {
        isZero: "0",
        isValid: "Dữ liệu hợp lệ",
        beginText: "Tài sản ",
        couterImport: " Tài sản được nhập khẩu thành công",
        couterUnImport: " Tài sản nhập khẩu thất bại"
    },

    delete: {
        beginText: "Bạn có muốn xóa tài sản << ",
        endText: " >>",
        mutilDelete: " tài sản đã được chọn. Bạn có muốn xóa các tài sản này ra khỏi danh sách?",
    }
}

export default resource;