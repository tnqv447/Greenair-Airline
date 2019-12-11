(function ($) {
    $(function () {
        $(".DeleteMaker").click(function () {
            var id = $(this).attr("id");
            if (confirm('Are you sure you want to delete this item has id: ' + id)) {
                $.ajax({
                    type: 'POST',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Maker?handler=DeleteMaker',
                    data: JSON.stringify({
                        MakerId: id
                    }),
                    success: function (respone) {
                        alert(respone);
                        location.reload();
                    }
                });
            }
        });

        $(".EditMaker").click(function () {
            var id = $(this).attr("id");
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Maker?handler=EditMaker',
                data: {
                    id: id
                },
                success: function (result) {
                    $("#EditMaker-name").val(result.MakerName);
                    $("#EditMaker-address").val(result.Address);
                    $("#EditMaker-id").val(result.MakerId);

                }
            });
        });
        $("#btsubmitEditMaker").click(function () {
            var id = $('#EditMaker-id').val();
            var name = $("#EditMaker-name").val();
            var address = $("#EditMaker-address").val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Maker?handler=EditMaker',
                data: JSON.stringify({
                    MakerId: id,
                    MakerName: name,
                    Address: address
                }),
                success: function (respone) {
                    $('#EditMaker').modal('hide');
                    alert(respone);
                    // $("#tableMaker").empty();
                    location.reload();
                    // $('#tableMaker').load("/Admin/Maker" + "  #tableMaker");
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        $("#btsubmitCreateMaker").click(function () {
            alert("Create");
            // var id = $('#CreateMaker-id').val();
            var name = $("#CreateMaker-name").val();
            var address = $("#CreateMaker-address").val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Maker?handler=CreateMaker',
                data: JSON.stringify({
                    MakerId: id,
                    MakerName: name,
                    Address: address
                }),
                success: function (respone) {
                    // $('#CreateMaker').modal('hide');
                    if (respone.trim() == "True") {
                        alert("Create success");
                        location.reload();
                    } else {
                        alert("This Id exists");
                        $('#CreateMaker-id').focus();
                    }
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });

    });
})(jQuery);