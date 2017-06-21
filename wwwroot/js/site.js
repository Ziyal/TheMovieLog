// LIST PAGE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 

// Display search box on click
$(".show_search_container").on("click", function(e){
    e.preventDefault();
    $(".show_search_container").fadeOut();
    $(this).next(".search_container").fadeIn();
})

$(document).on("click", ".search_movie", search);

// Make API call and display results
function search() {
    $(".search_results").empty();

    var movie = $(".input_movie").val().trim();
    var query = "https://api.themoviedb.org/3/search/movie?api_key=eab968ac592b5893e49606022d016228&language=en-US&query=" + movie + "&page=1&include_adult=false"

    $.ajax({
        type: "GET",
        url: query
    }).done(function(response){

        $(".results_title").empty();
        $(".results_title").append("<h3>Which movie?</h3><hr>");
        $('.container').css('height', '');

        for(var item = 0; item < 10; item++) {
            // $(".search_results").append("<h3 class='result_title'>"+response.results[item].title+"</h3>");
            // $(".search_results").append("<p class='result_release'>"+response.results[item].release_date+"</p>");
            // $(".search_results").append("<p class='result_select response_summary'>"+response.results[item].overview+"</p>");
            // $(".search_results").append("<img src='https://image.tmdb.org/t/p/w500"+response.results[item].poster_path+"' style='max-width:200px;max-height:150px' class='result_image'>"); 
            // $(".search_results").append("<button name='button' class='result_select btn btn-default result_btn' movie_id='"+response.results[item].id+"'movie_title='"+response.results[item].title+"' movie_release='"+response.results[item].release_date+"'>Choose Film</button><hr>");

            $(".search_results").append("<div class='result_text_container'><h3 class='result_title'>"+response.results[item].title+"</h3><p class='result_release'>"+response.results[item].release_date+"</p><p class='result_select response_summary'>"+response.results[item].overview+"</p><br><button name='button' class='result_select btn btn-default result_btn' movie_id='"+response.results[item].id+"'movie_title='"+response.results[item].title+"' movie_release='"+response.results[item].release_date+"'>Choose Film</button></div><img src='https://image.tmdb.org/t/p/w500"+response.results[item].poster_path+"' style='max-width:200px;max-height:150px' class='result_image'><hr>")
        }
    });
}

