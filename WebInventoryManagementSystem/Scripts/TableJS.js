$(document).ready(function () {

    $(".click").click(function () {

        var suppname = $("#Supplier").val();
        var proName = $("#ProductName").val();
        var proBarcode = $("#ProductBarcode").val();
        var proExpiry = $("#exdatePicker").val();
        var proCartons = $("#CartonTxt").val();
        var proPerCart = $("#pieceTxt").val();
        var proBuying = $("#buyingTxt").val();

        var code = "<tr><td><input type='checkbox' name='record'/> </td><td>" + suppname + "</td><td>" + proName + "</td><td>" + proBarcode + "</td><td>" + proExpiry + "</td><td> " + proCartons + "</td><td>" + proPerCart + "</td><td>" + proBuying + "</td></tr>";

        $("table .tbody").append(code);
    })

    $(".del").click(function () {
        $("table .tbody").find('input[name="record"]').each(function () {
            if($(this).is(":checked"))
            {
                $(this).parents("tr").remove();
            }
        })
    });
});
