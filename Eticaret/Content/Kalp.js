$(function () {
    $("#urundetay").on("click", ".kalp", function () {
        var id = $(this).data("id");
        if (this.id="begendi") {
            this.id = "begenmedi";
            $("#begenmedi").css("background-color","#999999");
        }
        $.ajax({
            type: "POST",
            url: "/urundetay/begen/" + id,
            success: function () {
                console.log("başarılı.");
            }
        });
    });
});


$(function () {
    $("#urundetay").on("click", "#begenmedi", function () {
        var id = $(this).data("id");
        this.id = "begendi";
        $("#begendi").css("background-color", "red");
    });
});

$(function () {
    $(".Yorumyap").click(function () {
        debugger;
        var id = $(this).data("id");
        $.ajax({
            type: "POST",
            url: "/urundetay/Yorumyap/" + id,
            data: "",
            success: function () {
                console.log("başarılı.");
            }
        });
    });
});

$(function () {
    $(".AnasayfaSepeteEkle").click(function myfunc() {
        var fid = $(this).data("id");
  
        debugger;
        $.ajax({
            type: "GET",
            url: "/Cart/AddToCart/" + fid,
           
            success: function mysepeteekleme() {

                $.ajax({
                    type: "GET",
                    url: "/Cart/SepetOzet",
                    success: function ozet(veri) {
                        $(".ps-cart").html('');
                        $(".ps-cart").html(veri);
                        alertify.success("Ürün Eklendi");
                    }

                });
            },
            error: function() {
                alert("ürün sepete eklenmedi");
            }
        });
    });
});

$(function () {
    $('.ps-rating').barrating({
        theme: 'fontawesome-stars'
    });
});