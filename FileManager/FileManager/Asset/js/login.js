$(document).ready(function () {
    $('#registerUser').validate({

        rules: {
            Username: "required",
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 5
            },
            repassword: {
                required: true,
                equalTo: "#Password"
            }
        },

        messages: {
            Username: "Bạn chưa nhâp tên đăng nhập",
            Password: {
                required: "Bạn chưa nhập mật khẩu",
                minlength: "Mật khẩu phải ít nhất 5 ký tự"
            },
            repassword: {
                required: "Bạn chưa nhập lại mật khẩu",
                equalTo: 'Mật khẩu nhập lại không trùng'
            },
            Email: {
                required: "Bạn chưa nhập email",
                email: "Email không hợp lệ"
            },
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
    $('#ChangePassword').validate({

        rules: {
            exPass: "required",
            newPass: {
                required: true,
                minlength: 5
            },
            renewPass: {
                required: true,
                equalTo: "#newPass"
            }
        },

        messages: {
            exPass: "Bạn chưa nhâp mật khẩu cũ",
            newPass: {
                required: "Bạn chưa nhập mật khẩu mới",
                minlength: "Mật khẩu phải ít nhất 5 ký tự"
            },
            renewPass: {
                required: "Bạn chưa nhập lại mật khẩu mới",
                equalTo: 'Mật khẩu nhập lại không trùng'
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });

});

$(function () {
    //nếu không có thao tác gì thì ẩn đi
    $('#AlertBox').removeClass('hide');

    //Sau khi hiển thị lên thì delay 1s và cuộn lên trên sử dụng slideup
    $('#AlertBox').delay(8000).slideUp(2000);
});