var width = $(window).width();

$(window).resize(function () {
    width = $(window).width();
    if (width >= 750) {
        $("#mobile-navigation-toggle").attr('aria-expanded', 'false');
        $("#main").animate({ marginLeft: '0px' }, 'slow');
    }
});

$("#mobile-navigation-toggle").click(function () {
    var expanded = $(this).attr('aria-expanded');

    if (expanded == "true") {
        $(this).attr('aria-expanded', 'false');
        $("#main").animate({ marginLeft: '0px' }, 'slow');
    }
    else {
        $(this).attr('aria-expanded', 'true');
        $("#main").animate({ marginLeft: '260px' }, 'slow');
    }

    $(".navbar-toggle").blur();
});

$("#copyright-year").html(new Date().getFullYear());

$(".delete-image").click(function (e) {
    e.preventDefault();
    var dataImageID = $(this).data('image-id');

    sweetAlert({
        title: "Delete this image?",
        text: "If you continue, you will permanently delete this image.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3156E7",
        confirmButtonText: "Delete",
        cancelButtonText: "Keep My Image",
        closeOnConfirm: true,
        closeOnCancel: true,
    },
    function (isConfirm) {
        if (isConfirm) {

            $.ajax({
                url: '/Images/Delete/',
                data: { id: dataImageID },
                cache: false,
                type: "POST",
                success: function (data) {
                    window.location.reload();
                },
                error: function (response) {
                    console.log("Error: " + response);
                }
            });
        }
    })
});

$(".delete-blog").click(function (e) {
    e.preventDefault();
    var dataBlogID = $(this).data('blog-id');

    sweetAlert({
        title: "Delete this blog?",
        text: "If you continue, you will permanently delete this blog and it will disappear from the website.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#C07F28",
        confirmButtonText: "Delete",
        cancelButtonText: "Keep My Blog",
        closeOnConfirm: true,
        closeOnCancel: true,
    },
    function (isConfirm) {
        if (isConfirm) {

            $.ajax({
                url: '/Blog/Delete/',
                data: { id: dataBlogID },
                cache: false,
                type: "POST",
                success: function (data) {
                    window.location.reload();
                },
                error: function (response) {
                    console.log("Error: " + response);
                }
            });
        }
    })
});

$(".delete-project").click(function (e) {
    e.preventDefault();
    $(".delete-blog").click(function (e) {
        e.preventDefault();
        var dataBlogID = $(this).data('blog-id');

        sweetAlert({
            title: "Delete this blog?",
            text: "If you continue, you will permanently delete this blog and it will disappear from the website.",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#C07F28",
            confirmButtonText: "Delete",
            cancelButtonText: "Keep My Blog",
            closeOnConfirm: true,
            closeOnCancel: true,
        },
        function (isConfirm) {
            if (isConfirm) {

                $.ajax({
                    url: '/Blog/Delete/',
                    data: { id: dataBlogID },
                    cache: false,
                    type: "POST",
                    success: function (data) {
                        window.location.reload();
                    },
                    error: function (response) {
                        console.log("Error: " + response);
                    }
                });
            }
        })
    });

    sweetAlert({
        title: "Delete this project?",
        text: "If you continue, you will permanently delete this project and it will disappear from the website.",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#C07F28",
        confirmButtonText: "Delete",
        cancelButtonText: "Keep My Project",
        closeOnConfirm: true,
        closeOnCancel: true,
    },
    function (isConfirm) {
        if (isConfirm) {

            $.ajax({
                url: '/Project/Delete/',
                data: { id: C },
                cache: false,
                type: "POST",
                success: function (data) {
                    window.location.reload();
                },
                error: function (response) {
                    console.log("Error: " + response);
                }
            });
        }
    })
});

$(document).ready(function () {
    if($("#BlogDate").length) {
        $("#BlogDate").datepicker({dateFormat: 'dd/mm/yy'});
    }

    if($("#ProjectPostDate").length) {
        $("#ProjectPostDate").datepicker({dateFormat: 'dd/mm/yy'});
    }
});