var grid = $('#grid').grid({
    primaryKey: 'ScenarioID',
    dataSource: '/Home/Get',
    grouping: { groupBy: 'UserID' },
    pager: { limit: 20, sizes: [20, 50, 100, 500] },
    columns: [{ field: 'ScenarioID', title: 'Scenario ID' }, { field: 'Name' }, { field: 'Surname' }, { field: 'Forename' }, { field: 'SampleDate', title: 'Sample Date' }, { field: 'CreationDate', title: 'Creation Date' }, { field: 'NumMonths', title: 'Num Months' }, { field: 'MarketID', title: 'Market ID' }, { field: 'NetworkLayerID', title: 'Network Layer ID' }]
});
grid.on('dataBound', function (e, records, totalRecords) {
    //grid.collapseAll();
    $("#grid tbody tr[role='group']").each(function () {
        var username = $(this).next().children("td:nth-child(4)").children("div").text() + " " + $(this).next().children("td:nth-child(5)").children("div").text();
        $(this).children("td:nth-child(2)").append("<div class='user-name'>User Name: " + username + "</div>");
        $(this).children("td:nth-child(2)").children("div[data-role='display']").hide();
    });
});
$('.select2me').select2({
    placeholder: "Select",
    allowClear: true
});
$(".select-sampledate").change(function (e) {
    var count = $(this).children("option:selected").data("count");
    var date = $(this).children("option:selected").text();
    $(".purple-plum .number").text(count);
    $(".purple-plum .desc").text(date);
});
$(".select-username").change(function (e) {
    var count = $(this).children("option:selected").data("count");
    var name = $(this).children("option:selected").text();
    $(".green-haze .number").text(count);
    $(".green-haze .desc").text(name);
});
$(".green-haze .more").click(function () {
    var selected_user_id = $(".select-username").children("option:selected").val();
    if (selected_user_id) {
        $("#grid tbody tr[role='group']").each(function () {
            var current_user_id = $(this).children("td:nth-child(2)").children("div[data-role='display']").text().split(":")[1].trim();
            if (current_user_id == selected_user_id) {
                $(this).children("td:nth-child(1)").children("div").trigger("click");
            }
        });
    }
});

$(".purple-plum .more").click(function () {
    var selected_date = $(".select-sampledate").children("option:selected").text();
    if (selected_date) {
        $("#grid tbody tr[data-role='row']").children("td:nth-child(6)").each(function () {
            var current_date = $(this).children("div[data-role='display']").text();
            if (selected_date == current_date) {
                $(this).parent().toggle();
            }
        });
    }
});
