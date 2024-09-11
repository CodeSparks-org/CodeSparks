document.getElementById('spark-categories').addEventListener('change', function (e) {
    const defaultName = "Spark";
    const exludedCategory = e.target.options[0].textContent;
    let selectedCategory = e.target[e.target.selectedIndex].textContent;

    if (selectedCategory === exludedCategory) {
        selectedCategory = defaultName;
    }

    document.getElementById('selectedCategoryText').textContent = selectedCategory;
});

const hashtagInput = document.getElementById('hashtag-input');
const hashtagList = document.getElementById('hashtag-list');
const hashtagHiddenInput = document.getElementById('hashtag-hidden-input');
const addTagButton = document.getElementById('addTagButton');

const fiedls = document.querySelectorAll('.create-spark-field');

if (fiedls?.length) {
    for (let i = 0; i < fiedls.length; i++) {
        fiedls[i].addEventListener('keydown', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault();
            }
        })
    }
}

hashtagInput.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
        e.preventDefault();
        addTag();
    }
});

addTagButton.addEventListener("click", function () {
    addTag();
});

function addTag() {
    const hashtag = hashtagInput.value.trim();
    const isElementsExist = hashtagList && hashtagHiddenInput;

    if (isElementsExist && hashtag) {
        const div = document.createElement('div');
        div.classList.add('hashtag-item', 'me-2');

        const spanTxt = document.createElement('span');
        spanTxt.classList.add('txt');
        spanTxt.textContent = `${hashtag}`;
        div.append(spanTxt);

        const closeBtn = document.createElement('button');
        closeBtn.classList.add('close');

        closeBtn.addEventListener('click', function (e) {
            e.preventDefault();

            hashtagHiddenInput.value = hashtagHiddenInput.value
                .split(',')
                .filter(str => str !== hashtag)
                .join(',');

            div.remove();
        });

        div.append(closeBtn);

        hashtagList.append(div);

        hashtagHiddenInput.value += (hashtagHiddenInput.value ? "," : "") + hashtag;
        hashtagInput.value = '';
    }
}