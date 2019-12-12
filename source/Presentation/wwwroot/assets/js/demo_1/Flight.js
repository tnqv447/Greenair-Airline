(function ($) {
    $(function () {
        $(".DeleteFlight").click(function () {
            var id = $(this).attr("id").trim();
            alert(id);
            if (confirm('Are you sure you want to delete this flight has id: ' + id)) {
                $.ajax({
                    type: 'POST',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Flight?handler=DeleteFlight',
                    data: JSON.stringify({
                        FlightId: id
                    }),
                    success: function (respone) {
                        alert(respone);
                        location.reload();
                    }
                });
            }
        });
        $(".DetailFlight").click(function () {
            var id = $(this).attr("id");
            // alert("Id1 " + id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?handler=DetailFlight',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.FlightId);
                    $("#DetailFlight-planeid").text(result.flight.planeId);
                    $("#DetailFlight-flightid").text(result.flight.flightId);
                    $("#DetailFlight-status").text(result.flight.status);
                    var s = "<hr>";
                    for (item of result.listFlight) {
                        s += `<div class"row">`;

                        s += `<div class="form-group row">`;
                        s += ` <label class="col-sm-2 col-form-label">Detail ID:</label><div class="col-sm-2">`;
                        s += `<span>` + item.flightDetailId + `</span></div>`;
                        s += ` <label class="col-sm-2 col-form-label">Departure Date:</label><div class="col-sm-2">`;
                        s += `<span>` + item.depDate + `</span></div>`;
                        s += ` <label class="col-sm-2 col-form-label">Arrive Date:</label><div class="col-sm-2">`;
                        s += `<span>` + item.arrDate + `</span></div>`;
                        s += `</div>`;

                        s += `<div class="form-group row">`;
                        s += ` <label class="col-sm-2 col-form-label">Route ID:</label><div class="col-sm-2">`;
                        s += `<span>` + item.routeId + `</span></div>`;
                        s += ` <label class="col-sm-2 col-form-label">Origin:</label><div class="col-sm-2">`;
                        s += `<span>` + item.originAirport + ` (` + item.originCountry + `)` + `</span></div>`;
                        s += ` <label class="col-sm-2 col-form-label">Destination:</label><div class="col-sm-2">`;
                        s += `<span>` + item.desAirport + ` (` + item.desCountry + `)` + `</span></div>`;
                        s += `</div>`;

                        s += `</div><hr>`;
                    }
                    $("#DetailFlight-context").html(s);
                }
            });
        });



        // form create
        $(document).on('focus', '.choose_date', function () {
            $(this).datetimepicker({
                timeFormat: "hh:mm TT",
                dateFormat: "dd-mm-yy",
                minDate: new Date()
            });
        });
        // $(".choose_date").live("focus", function () {
        //     $(this).removeClass(".choose_date").datetimepicker({
        //         timeFormat: "hh:mm TT",
        //         dateFormat: "dd-mm-yy",
        //         minDate: new Date()
        //     }).focus();
        //     return false;
        // })

        function loadRoute() {
            var html = "";
            var num = parseInt($("#CreateFlight-number").val());
            var routeid = $("#CreateFlight-routeid" + num).val();
            // alert(num + " " + routeid);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?Handler=Routes',
                data: {
                    routeid: routeid
                },
                success: function (response) {
                    for (var i = 0; i < response.length; i++) {
                        html += `<option >` + response[i].routeId + `: ` + response[i].origin + ` - ` + response[i].destination + `</option>`;
                    }
                    // $(".list").html(html);
                    // $(".listEdit" + (num + 1)).append(html);
                    $(".list" + (num + 1)).html(html);
                }
            });
            return html;
        }

        function loadDateTime() {
            var html = "";
            var num = parseInt($("#CreateFlight-number").val());
            var arrdate = $("#CreateFlight-arrdate" + num).val();
            var route = $("#CreateFlight-routeid" + num).val();
            route = route.slice(0, 5);
            // alert(arrdate);
            if (arrdate == "") return;
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?Handler=DateTimes',
                data: {
                    arrdate: arrdate,
                    routeid: route
                },
                success: function (response) {
                    // alert(response);
                    if (response != "null") {
                        $("#CreateFlight-depdate" + (num + 1)).val(response.depDate);
                        $("#CreateFlight-arrdate" + (num + 1)).val(response.arrDate);
                    }


                }
            });
        }

        $(document).on("click", "#CreateFlight-btadd", function () {
            var num = parseInt($("#CreateFlight-number").val());
            num = num + 1;
            loadRoute();
            if (num <= 4) {
                var html = "";
                html += `<div class="row" id="CreateFlight-row` + num + `">
                    <div class="col-md-4">`;
                html += `    <select class="form-control form-control-lg list` + num + ` CreateFlight-routeid" id="CreateFlight-routeid` + num + `">
                            </select>`;
                html += `    </div>
                    <div class="col-md-4">`;
                html += `      <input id="CreateFlight-depdate` + num + `" type="text" class="form-control CreateFlight-depdate choose_date " />`;
                html += `                  <span style="color:red" id="error-date` + num + `" class=" hidden-class"> Please choose date</span>
                `
                html += `    </div>
                    <div class="col-md-4">`;
                html += `      <input id="CreateFlight-arrdate` + num + `" type="text" class="form-control CreateFlight-arrdate choose_date" disabled/>`;
                html += `    </div>
                    </div><hr id="CreateFlight-hr` + num + `"/>`;
                $("#CreateFlight-context").append(html);
                loadDateTime();
                $("#CreateFlight-number").val(num);
            }
        });
        $("#CreateFlight-btdelete").click(function () {
            var num = parseInt($("#CreateFlight-number").val());
            if (num <= 1) return;
            var s = "CreateFlight-row" + num;
            $("#" + s).remove();
            s = "CreateFlight-hr" + num;
            $("#" + s).remove();
            num = num - 1;
            $("#CreateFlight-number").val(num);
        });
        $(document).on('change', '.CreateFlight-routeid', function () {
            var id = $(this).attr("id");
            var num = parseInt(id.slice(id.length - 1, id.length));
            var text = $(this).val().slice(0, 5);
            // alert(id + " " + num + " " + text);
            var depDate = $("#CreateFlight-depdate" + num).val();
            if (depDate != "") {
                $.ajax({
                    type: 'GET',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Flight?Handler=CalArrDate',
                    data: {
                        routeid: text,
                        depDate: depDate
                    },
                    success: function (response) {
                        $("#CreateFlight-arrdate" + num).val(response);
                        //alert(response);
                    }
                });
            }

        });
        $(document).on('change', '.CreateFlight-depdate', function () {
            var id = $(this).attr("id");
            var num = parseInt(id.slice(id.length - 1, id.length));
            var text = $("#CreateFlight-routeid" + num).children("option:selected").val().slice(0, 5);
            var depDate = $(this).val();
            if (depDate != "" && text != "") {
                $.ajax({
                    type: 'GET',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Flight?Handler=CalArrDate',
                    data: {
                        routeid: text,
                        depDate: depDate
                    },
                    success: function (response) {
                        $("#CreateFlight-arrdate" + num).val(response);
                        //alert(response);
                    }
                });
            }
        });
        $("#btsubmitCreateFlight").click(function () {
            // alert("Create");
            event.preventDefault();
            var planeid = $("#CreateFlight-planeid").val();
            var planeid = $("#CreateFlight-planeid").val().slice(planeid.length - 5, planeid.length);
            var status = $("#CreateFlight-status").val();
            var num = parseInt($("#CreateFlight-number").val());
            var routeId = [];
            var depDate = [];
            var arrDate = [];
            for (var i = 1; i <= num; ++i) {
                routeId[i - 1] = $("#CreateFlight-routeid" + i).val().slice(0, 5);
                depDate[i - 1] = $("#CreateFlight-depdate" + i).val();
                if (depDate[i - 1] == "") {
                    $("#error-date" + i).removeClass("hidden-class");
                    return;
                } else {
                    $("#error-date" + i).addClass("hidden-class");
                }
                arrDate[i - 1] = $("#CreateFlight-arrdate" + i).val();
            }
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?handler=CreateFlight',
                data: JSON.stringify({
                    PlaneId: planeid,
                    Status: status,
                    routeId: routeId,
                    depDate: depDate,
                    arrDate: arrDate
                }),
                success: function (respone) {
                    // $('#CreateMaker').modal('hide');
                    if (respone.trim() == "True") {
                        alert("Create success");
                        location.reload();
                    } else {
                        alert("This Id exists");
                        // $('#CreateMaker-id').focus();
                    }
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });

        // edit form
        $(".EditFlight").click(function () {
            var id = $(this).attr("id");
            // alert(id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?handler=EditFlight',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.flight.flightId);
                    var n = result.routeId.length;
                    loadRouteFull(n);
                    $("#EditFlight-flightid").val(result.flightId);
                    $("#EditFlight-planeiddefault").text(result.planeId);
                    $("#EditFlight-statusdefault").text(result.status);
                    var num = 0;
                    var s = "";
                    for (var i = 0; i < n; ++i) {
                        num++;
                        s += `<div class="row" id="EditFlight-row` + num + `">`;
                        s += ` <input id="EditFlight-flightdetailid` + num + `" type="text" class="form-control form-control-lg" value="` + result.flightDetailId[i] + `" hidden/>`;
                        s += `  <div class="col-md-4">`;
                        s += `<select class="form-control form-control-lg EditFlight-routeid listEdit` + num + ` EditFlight-routeid" id="EditFlight-routeid` + num + `">`;
                        s += `<option hidden>` + result.routeId[i] + `</option>`;
                        s += `</select></div>`;
                        s += `<div class="col-md-4"><input id="EditFlight-depdate` + num + `" type="text" class="form-control EditFlight-depdate choose_date" value="` + result.depDate[i] + `"/></div>`;
                        s += `<span style="color:red" id="error-dateEdit` + num + `" class=" hidden-class"> Please choose date</span>`;
                        s += `<div class="col-md-4"><input id="EditFlight-arrdate` + num + `" type="text" class="form-control EditFlight-arrdate choose_date" value="` + result.arrDate[i] + `" disabled/></div>`;
                        s += `</div>`;
                        s += `<hr id="EditFlight-hr` + num + `">`;
                    }
                    $("#EditFlight-number").val(num);
                    $("#EditFlight-context").html(s);
                }
            });
        });
        $("#btsubmitEditFlight").click(function () {
            alert("Edit");
            event.preventDefault();
            var flightid = $("#EditFlight-flightid").val();
            var planeid = $("#EditFlight-planeid").val();
            var planeid = $("#EditFlight-planeid").val().slice(planeid.length - 5, planeid.length);
            var status = $("#EditFlight-status").val();
            var num = parseInt($("#EditFlight-number").val());
            var routeId = [];
            var depDate = [];
            var arrDate = [];
            var flightdetailId = [];
            for (var i = 1; i <= num; ++i) {
                flightdetailId[i - 1] = $("#EditFlight-flightdetailid" + i).val();
                routeId[i - 1] = $("#EditFlight-routeid" + i).val().slice(0, 5);
                depDate[i - 1] = $("#EditFlight-depdate" + i).val();
                if (depDate[i - 1] == "") {
                    $("#error-dateEdit" + i).removeClass("hidden-class");
                    return;
                } else {
                    $("#error-dateEdit" + i).addClass("hidden-class");
                }
                arrDate[i - 1] = $("#EditFlight-arrdate" + i).val();
            }
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?handler=EditFlight',
                data: JSON.stringify({
                    FlightId: flightid,
                    PlaneId: planeid,
                    Status: status,
                    flightDetailId: flightdetailId,
                    routeId: routeId,
                    depDate: depDate,
                    arrDate: arrDate
                }),
                success: function (respone) {
                    // $('#EditMaker').modal('hide');
                    if (respone.trim() == "True") {
                        alert("Edit success");
                        location.reload();
                    } else {
                        alert("This Id exists");
                        // $('#EditMaker-id').focus();
                    }
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        $("#EditFlight-btadd").click(function () {
            var num = parseInt($("#EditFlight-number").val());
            num = num + 1;
            loadRouteEdit();
            if (num > 4) return;
            var html = "";
            html += `<div class="row" id="EditFlight-row` + num + `">`;
            html += ` <input id="EditFlight-flightdetailid` + num + `" type="text" class="form-control form-control-lg" value="" hidden/>`;
            html += `    <div class="col-md-4">`;
            html += `    <select class="form-control form-control-lg EditFlight-routeid listEdit` + num + `" id="EditFlight-routeid` + num + `">
                        </select>`;
            html += `    </div>
                <div class="col-md-4">`;
            html += `      <input id="EditFlight-depdate` + num + `" type="text" class="form-control EditFlight-depdate choose_date " />`;
            html += `<span style="color:red" id="error-dateEdit` + num + `" class=" hidden-class"> Please choose date</span>`;
            html += `    </div>
                <div class="col-md-4">`;
            html += `      <input id="EditFlight-arrdate` + num + `" type="text" class="form-control" disabled/>`;
            html += `    </div>
                </div><hr id="EditFlight-hr` + num + `"/>`;
            $("#EditFlight-context").append(html);
            loadDateTimeEdit();
            $("#EditFlight-number").val(num);
        });
        $("#EditFlight-btdelete").click(function () {
            var num = parseInt($("#EditFlight-number").val());
            if (num <= 1) return;
            var s = "EditFlight-row" + num;
            $("#" + s).remove();
            s = "EditFlight-hr" + num;
            $("#" + s).remove();
            num = num - 1;
            $("#EditFlight-number").val(num);
        });
        $(document).on('change', '.EditFlight-routeid', function () {
            var id = $(this).attr("id");
            var num = parseInt(id.slice(id.length - 1, id.length));
            var text = $(this).val().slice(0, 5);
            // alert(id + " " + num + " " + text);
            var depDate = $("#EditFlight-depdate" + num).val();
            if (depDate != "") {
                $.ajax({
                    type: 'GET',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Flight?Handler=CalArrDate',
                    data: {
                        routeid: text,
                        depDate: depDate
                    },
                    success: function (response) {
                        $("#EditFlight-arrdate" + num).val(response);
                        //alert(response);
                    }
                });
            }

        });
        $(document).on('change', '.EditFlight-depdate', function () {
            var id = $(this).attr("id");
            var num = parseInt(id.slice(id.length - 1, id.length));
            var text = $("#EditFlight-routeid" + num).children("option:selected").val().slice(0, 5);
            var depDate = $(this).val();
            if (depDate != "" && text != "") {
                $.ajax({
                    type: 'GET',
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Flight?Handler=CalArrDate',
                    data: {
                        routeid: text,
                        depDate: depDate
                    },
                    success: function (response) {
                        $("#EditFlight-arrdate" + num).val(response);
                        //alert(response);
                    }
                });
            }
        });

        function loadRouteFull(num) {
            var html = "";
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?Handler=RouteFull',
                success: function (response) {
                    for (var i = 0; i < response.length; i++) {
                        html += `<option >` + response[i].routeId + `: ` + response[i].origin + ` - ` + response[i].destination + `</option>`;
                    }
                    for (var i = 1; i <= num; ++i) {
                        $(".listEdit" + i).append(html);
                    }

                }
            });
            return html;
        }

        function loadRouteEdit() {
            var html = "";
            var num = parseInt($("#EditFlight-number").val());
            var routeid = $("#EditFlight-routeid" + num).val();
            // alert(num + " " + routeid);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?Handler=Routes',
                data: {
                    routeid: routeid
                },
                success: function (response) {
                    for (var i = 0; i < response.length; i++) {
                        html += `<option >` + response[i].routeId + `: ` + response[i].origin + ` - ` + response[i].destination + `</option>`;
                    }
                    $(".listEdit" + (num + 1)).html(html);
                }
            });
            return html;
        }

        function loadDateTimeEdit() {
            var html = "";
            var num = parseInt($("#EditFlight-number").val());
            var arrdate = $("#EditFlight-arrdate" + num).val();
            var route = $("#EditFlight-routeid" + num).val();
            route = route.slice(0, 5);
            // alert(arrdate);
            if (arrdate == "") return;
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Flight?Handler=DateTimes',
                data: {
                    arrdate: arrdate,
                    routeid: route
                },
                success: function (response) {
                    // alert(response);
                    if (response != "null") {
                        $("#EditFlight-depdate" + (num + 1)).val(response.depDate);
                        $("#EditFlight-arrdate" + (num + 1)).val(response.arrDate);
                    }


                }
            });
        }

        $(".choose").datepicker({
            dateFormat: 'dd/mm/yy',
            minDate: new Date(),
        })
        $(document).on("change", "#Search_date", function () {
            var searchString = $(this).val();
            $.ajax({
                type: "GET",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: "/Admin/Flight?Handler=SearchFlight",
                data: {
                    searchString: searchString
                },
                success: function (reponse) {
                    alert(reponse);
                    $("#TableFlight").empty();
                    $("#TableFlight").load("/Admin/Flight" + " #TableFlight");
                },
                error: function () {
                    alert("not ok");
                }
            });
        });




    });
})(jQuery);
$(document).ready(function () {

});