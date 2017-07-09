$(function() {
    $('.container')
        .on('click', 'button.delete-btn', handleDelete)
        .on('click', 'input.complete-chk', handleComplete)
        .on('click', 'button.add-btn', handleAdd);

    function handleAdd(e) {
        e.preventDefault();
        const name = $('#add-name').val();
        if (name !== null && name !== undefined) {
            const todo = { name };
            $.ajax({
                method: 'POST',
                url: '/AddTodo',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(todo),
                success: function (response) {
                    renderTodos(response);
                }
            });
        }
    }

    function handleComplete(e) {
        $(e.target).parent().attr('data-complete', !$(e.target).parent().attr('data-complete'));
        const item = getItem(e);
        if (item.id !== null || item.id !== undefined) {
            $.ajax({
                method: 'PUT',
                url: '/UpdateTodo',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(item),
                success: function (response) {
                    renderTodos(response);
                }
            });
        }
    }

    function handleDelete(e) {
        const item = getItem(e);
        if (item.id !== null || item.id !== undefined) {
            $.ajax({
                method: 'DELETE',
                url: '/DeleteTodo?id=' + item.id,
                success: function (response) {
                    renderTodos(response);
                }
            });
        }
    }

    function getItem(e) {
        const item = $(e.target).parent();
        const complete = !!item.attr('data-complete');
        return {
            id: parseInt(item.attr('data-id')),
            name: item.attr('data-name'),
            complete: complete
        };
    }

    function renderTodos(todos) {
        const $container = $('#todo-container');
        $container.empty();
        todos.forEach(function (todo) {
            const templateData = {
                todoId: todo.id,
                todoName: todo.name,
                isComplete: todo.complete,
                completedTodo: todo.complete ? "checked" : ""
            };
            const template = $('#todo-template').html();
            $container.append(todoTemplateString(template, templateData));
        });
    }

    function todoTemplateString(template, data) {
        Object.keys(data).map(function (key) {
            template = template.replace(new RegExp('##' + key + '##', 'g'), data[key]);
        });
        return template;
    }
});