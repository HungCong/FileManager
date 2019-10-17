var tool = {
    init: function () {
        tool.regEvents();
    },
    regEvents: function () {


        //Xóa file
        $('.btn-remove').off('click').on('click', function () {
            if (confirm('Bạn có thực sự muốn xóa file này không ?')) {

                $.ajax({
                    url: '/Upload/deleteFile',
                    data: {
                        fileID: $(this).data('file_id')
                    },
                    datatype: 'json',
                    type: 'post',
                    success: function (res) {
                        if (res.status == true) {
                            window.location.href = "/Home";
                            PNotify.success({
                                title: 'Thành công!',
                                text: 'Xóa file thành công'
                            });
                        } else {
                            PNotify.error({
                                title: 'Lỗi',
                                text: 'Xóa file không thành công!'
                            });
                        }

                    }
                });
            }
           
        });

        //Tải file
        $('.btn-download').off('click').on('click', function () {
            
                $.ajax({
                    url: '/Upload/downloadFile',
                    contentType: 'application/json; charset=utf-8',
                    data: {
                        fileID: $(this).data('file_id')
                    },
                    datatype: 'json',
                    type: 'get',
                    success: function (res) {
                        //window.location.href = "/Upload/downloadFile/" + fileID;
                    }
                });

        });
    }
}

tool.init();