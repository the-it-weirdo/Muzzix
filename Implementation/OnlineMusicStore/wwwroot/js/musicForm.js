$(document).ready(function() {
  $('#artist-multiselect').multiselect();
});

function formSubmit() {
  const formDataElements = document.getElementById("musicForm").elements;
  console.log(formDataElements);
  for (let elem in formDataElements) {
    console.log(elem, elem.value);
  }
  return true;
}
