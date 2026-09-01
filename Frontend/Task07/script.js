document.getElementById('loadBtn').addEventListener('click', () => {
    const todoList = document.getElementById('todoList');
    
    todoList.innerHTML = '<li class="list-group-item text-center text-muted py-3">Loading...</li>';

    fetch('https://jsonplaceholder.typicode.com/todos?_limit=6')
        .then(response => response.json())
        .then(todos => {
            todoList.innerHTML = ''; 

            todos.forEach(todo => {
                const isCompleted = todo.completed;
                
                const listItemClass = isCompleted 
                    ? 'list-group-item-success text-decoration-line-through text-muted' 
                    : '';
                
                const badgeClass = isCompleted ? 'bg-success' : 'bg-secondary';
                const statusText = isCompleted ? 'Done' : 'Pending';

                const li = document.createElement('li');
                li.className = `list-group-item d-flex justify-content-between align-items-center py-3 ${listItemClass}`;
                
                li.innerHTML = `
                    <span>${todo.title}</span>
                    <span class="badge ${badgeClass} rounded-pill px-3 py-2">${statusText}</span>
                `;
                
                todoList.appendChild(li);
            });
        })
        .catch(error => {
            console.error(error);
            todoList.innerHTML = '<li class="list-group-item text-center text-danger py-3">Failed to load todos.</li>';
        });
});
