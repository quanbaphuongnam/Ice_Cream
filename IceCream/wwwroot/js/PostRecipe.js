// Check for valid email syntax


//function closeForm() {
//    document.contactform.name.value = '';

//    document.contactform.message.value = '';


//    $('.name').removeClass('typing');
//    $('.message').removeClass('typing');

//    $('.cd-popup').removeClass('is-visible');
//    $('.notification').addClass('is-visible');
//    $('#notification-text').html("Thank you for sending a new recipe for us!");
//}

//$(document).ready(function ($) {

//    /* ------------------------- */
//    /* Contact Form Interactions */
//    /* ------------------------- */
//    $('#contact').on('click', function (event) {
//        event.preventDefault();

//        $('#contactblurb').html('Thank you for sending a new recipe for us, we will review your formula as soon as possible!');
//        $('.contact').addClass('is-visible');

//        if ($('#name').val().length != 0) {
//            $('.name').addClass('typing');
//        }

//        if ($('#message').val().length != 0) {
//            $('.message').addClass('typing');
//        }
//    });

//    //close popup when clicking x or off popup
//    $('.cd-popup').on('click', function (event) {
//        if ($(event.target).is('.cd-popup-close') || $(event.target).is('.cd-popup')) {
//            event.preventDefault();
//            $(this).removeClass('is-visible');
//        }
//    });

//    //close popup when clicking the esc keyboard button
//    $(document).keyup(function (event) {
//        if (event.which == '27') {
//            $('.cd-popup').removeClass('is-visible');
//        }
//    });

//    /* ------------------- */
//    /* Contact Form Labels */
//    /* ------------------- */
//    $('#name').keyup(function () {
//        $('.name').addClass('typing');
//        if ($(this).val().length == 0) {
//            $('.name').removeClass('typing');
//        }
//    });

//    $('#message').keyup(function () {
//        $('.message').addClass('typing');
//        if ($(this).val().length == 0) {
//            $('.message').removeClass('typing');
//        }
//    });

//    /* ----------------- */
//    /* Handle submission */
//    /* ----------------- */
//    $('#contactform').submit(function () {
//        var name = $('#name').val();

//        var message = $('#message').val();
//        var human = $('#human:checked').val();

//        if (human) {
//            if (message) {
//                if (name) {
                    

//                        // Handle submitting data somewhere
//                        // For a tutorial on submitting the form to a Google Spreadsheet, see:
//                        // https://notnaturaltutorials.wordpress.com/2016/03/20/submit-form-to-spreadsheet/

//                        /*
//                                    var googleFormsURL = "https://docs.google.com/forms/d/1dHaFG67d7wwatDtiVNOL98R-FwW1rwdDwdFqqKJggBM3nFB4/formResponse";
//                                    // replace these example entry numbers
//                                    var spreadsheetFields = {
//                                      "entry.212312005": name,
//                                      "entry.1226278897": email,
//                                      "entry.1835345325": message
//                                    }
//                                    $.ajax({
//                                      url: googleFormsURL,
//                                      data: spreadsheetFields,
//                                      type: "POST",
//                                      dataType: "xml",
//                                      statusCode: {
//                                        0: function() {
                        
//                                        },
//                                        200: function() {
                        
//                                        }
//                                      }
//                                    });
//                        */

//                        closeForm();

                  
//                } else {
//                    $('#notification-text').html('<strong>Please complete your post.</strong>');
//                    $('.notification').addClass('is-visible');
//                }
//            } else {
//                $('#notification-text').html('<strong>Please complete your post</strong>');
//                $('.notification').addClass('is-visible');
//            }
//        } else {
//            $('#notification-text').html('<h3><strong><em>Warning: Please prove you are a human and not a robot.</em></strong></h3>');
//            $('.notification').addClass('is-visible');
//        }

//        return false;
//    });
//});