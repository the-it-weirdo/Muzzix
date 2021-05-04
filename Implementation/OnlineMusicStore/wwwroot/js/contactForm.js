const rangeValueElement = document.getElementById("range-value");
const rangeInputElement = document.getElementById("Rating");

rangeValueElement.innerHTML = Rating.value;

function onRatingChange() {
  rangeValueElement.innerHTML = Rating.value;
}
