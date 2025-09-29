document.addEventListener('DOMContentLoaded', function () {
    // делегация по всем ссылкам с классом filter-link
    document.querySelectorAll('.filter-link').forEach(function (el) {
        el.addEventListener('click', function (e) {
            // если ссылка ведёт на ту же страницу и имеет href, предотвращаем переход
            e.preventDefault();

            const category = el.dataset.category || '';
            const type = el.dataset.type || '';
            const band = el.dataset.band || '';

            const params = new URLSearchParams();
            if (category) params.set('category', category);
            if (band) {
                // band имеет приоритет для ByBands
                params.set('band', band);
            } else if (type) {
                params.set('type', type);
            }

            const qs = params.toString();
            const url = `${location.pathname}${qs ? '?' + qs : ''}`;

            // pushState для красивого URL
            history.pushState({}, '', url);

            // подгружаем partial (ProductsPartial) — контроллер должен вернуть partial
            fetch(`/Home/ProductsPartial?${qs}`)
                .then(res => {
                    if (!res.ok) throw new Error('Network response was not ok');
                    return res.text();
                })
                .then(html => {
                    const grid = document.getElementById('products-grid');
                    if (grid) grid.innerHTML = html;

                    // обновим active классы
                    document.querySelectorAll('.filter-link, .category-link').forEach(a => a.classList.remove('active-filter'));
                    // пометим выбранные
                    if (category) {
                        const catAll = document.querySelector(`.category-link[href*="category=${category}"]`);
                        if (catAll) catAll.classList.add('active-filter');
                    }
                    if (band) {
                        const bandEl = document.querySelector(`.filter-link[data-band="${band}"]`);
                        if (bandEl) bandEl.classList.add('active-filter');
                    } else if (type) {
                        const typeEl = document.querySelector(`.filter-link[data-type="${type}"]`);
                        if (typeEl) typeEl.classList.add('active-filter');
                    }
                })
                .catch(err => {
                    console.error(err);
                    // fallback — обычная навигация
                    window.location.href = url;
                });
        });
    });

    // handle back/forward
    window.addEventListener('popstate', function () {
        // для простоты делаем reload, можно оптимизировать под fetch
        location.reload();
    });
});
