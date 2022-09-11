/**
 * =================================
 *       Creating form fields
 * =================================
 */
export function kreirajSelect(
  destinationForm,
  selectOptions,
  label,
  elementClass = ""
) {
  let selectContainer = document.createElement("div");
  selectContainer.classList = "select-kontejner";
  let selectLabel = document.createElement("label");
  selectLabel.innerText = label;
  let selectList = document.createElement("select");
  selectList.className = elementClass + " border p-1";

  selectContainer.appendChild(selectLabel);
  selectContainer.appendChild(selectList);
  destinationForm.appendChild(selectContainer);

  selectOptions.forEach((element) => {
    let option = document.createElement("option");
    option.value = element.id;
    option.innerHTML = element.ime;
    selectList.appendChild(option);
  });
}

export function kreirajDugme(klasa, text) {
  let button = document.createElement("button");
  button.className = klasa;
  button.innerText = text;
  return button;
}

export function kreirajInput(kontTip, klasa, placeholder, tip) {
  let selectContainer = document.createElement("div");
  selectContainer.classList = "input-kontejner";
  let selectLabel = document.createElement("label");
  selectLabel.innerText = placeholder;

  let inputPolje = document.createElement("input");
  inputPolje.className = klasa+" input";
  inputPolje.type = tip;
  inputPolje.placeholder = placeholder;
  selectContainer.appendChild(selectLabel);
  selectContainer.appendChild(inputPolje);
  kontTip.appendChild(selectContainer);
}
