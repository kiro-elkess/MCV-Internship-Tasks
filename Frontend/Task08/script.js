const API_KEY = '837be113847b4b380e8ec1938fac0d66';
const API_URL = `https://api.themoviedb.org/3/trending/movie/day?api_key=${API_KEY}&language=en-US`;
const IMAGE_BASE_URL = 'https://image.tmdb.org/t/p/w500';

const moviesContainer = document.getElementById('moviesContainer');

async function fetchTrendingMovies() {
    try {
        const response = await fetch(API_URL);
        const data = await response.json();
        
        if (data.results) {
            renderMovies(data.results);
        } else {
            moviesContainer.innerHTML = '<p class="text-white">Failed to load movies.</p>';
        }
    } catch (error) {
        console.error('Error fetching movies:', error);
        moviesContainer.innerHTML = '<p class="text-white text-center w-100">An error occurred while fetching movies.</p>';
    }
}

function renderMovies(movies) {
    moviesContainer.innerHTML = ''; // Clear container

    movies.forEach(movie => {
        // Create col div
        const col = document.createElement('div');
        col.className = 'col';

        // Format rating
        const rating = movie.vote_average ? movie.vote_average.toFixed(1) : 'N/A';
        const posterUrl = movie.poster_path ? `${IMAGE_BASE_URL}${movie.poster_path}` : 'https://via.placeholder.com/500x750?text=No+Image';

        // Create card HTML
        col.innerHTML = `
            <div class="movie-card shadow-sm" onclick="openMovie(${movie.id})">
                <img src="${posterUrl}" class="movie-poster" alt="${movie.title}">
                <div class="movie-info">
                    <h5 class="movie-title">${movie.title}</h5>
                    <p class="movie-overview">${movie.overview}</p>
                    <span class="movie-rating">${rating}</span>
                </div>
            </div>
        `;
        
        moviesContainer.appendChild(col);
    });
}

// Function to open TMDB page for the movie when card is clicked
function openMovie(movieId) {
    window.open(`https://www.themoviedb.org/movie/${movieId}`, '_blank');
}

// Fetch and display on load
fetchTrendingMovies();
