(function ($) {
    $(function () {
        loadUser();

        function loadUser() {
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Account?handler=User',
                success: function (response) {
                    if ($.trim(response.check) == "not") {

                        $("#personName").text(response.name);
                    }
                },
                error: function (response) {
                    alert("false");
                }
            })
        }
        $("#signout").on("click", function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Account?handler=LogOut",
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    loadUser();
                    location.replace("/admin");
                    // location.reload();
                }
            });
        });
    });
})(jQuery);