$(document).ready(function () {
     
    $(window).on('load', function () {

        resetCheckboxes();

    });

    function toggleColumn(checkboxId, columnName) {
        $(checkboxId).click(function () {
            $("." + columnName).toggle();
            localStorage.setItem(columnName, $(this).prop('checked'));

        });
    }

    function toggleState() {
        $(".toggle-checkbox").each(function () {
            var columnName = $(this).val();
            var isChecked = localStorage.getItem(columnName) === 'true';

            $(this).prop('checked', isChecked);
            if (isChecked) {
                $("." + columnName).show(); // Show the column
            } else {
                $("." + columnName).hide(); // Hide the column
            }
        });
    }

    // Function to reset checkboxes to their initial state
    function resetCheckboxes() {
        $(".toggle-checkbox").prop('checked', true);
        $(".toggle-checkbox").each(function () {
            var columnName = $(this).val();
            localStorage.setItem(columnName, true);
            $("." + columnName).show();
        });
    }

    //  reset button
    $("#resetCheckboxes").click(function () {
        resetCheckboxes();
    });

    toggleColumn("#fname", "fname");
    toggleColumn("#lname", "lname");
    toggleColumn("#course", "course");
    toggleColumn("#email", "email");
    toggleColumn("#phone", "phone");
    toggleColumn("#gender", "gender");
    toggleState();

   


    $("#searchBox").click(function (e) {
        e.preventDefault();

        var search = $("input[name='search']").val();
        
       
       

        $.ajax({
            url: "/StudentData/Index",
            method: "Get",
            data: { search: search },
            success: function (data) {

                setTimeout(function () {
                    $('#ViewContainer').html(data);
                    toggleState();
                },500);
            },

            error: function () {
                console.log("failed to load");
            }
        });

    });
   

    //Implementing ajax for pagination

    $(".page-link").click(function (e) {
        e.preventDefault();


        var page = $(this).data("page");
        var search = $("input[name='search']").val();
        $("#ViewContainer").html("<span class='spinner-border text-warning loadIcon' role='status' style='margin-left:75vh; border-width:5px; width:80px; height:80px;' ></span > ");
             
        $.ajax({
            url: "/StudentData/Index",
            method: "GET",
            data: { search: search, page: page },
            success: function (data) { 
                setTimeout(function () {
                   
                    $("#ViewContainer").html(data);
                    toggleState();
                }, 500);
            },
            error: function () {
                console.error("Failed to load the partial view.");
              
            }
        });
    });


});