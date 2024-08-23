document.getElementById('spark-categories').addEventListener('change', function(e) {
  const defaultName = "Spark";
  const exludedCategory = e.target.options[0].textContent;
  let selectedCategory = e.target[e.target.selectedIndex].textContent;

  if (selectedCategory === exludedCategory) {
      selectedCategory = defaultName;
  }
  
  document.getElementById('selectedCategoryText').textContent = selectedCategory;
});