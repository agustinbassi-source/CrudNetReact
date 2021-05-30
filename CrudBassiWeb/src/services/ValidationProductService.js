const validateProduct = (model) => {

  let errors = [];

 // Name
 
  if (model.name != undefined && model.name != null && model.name.length > 50) {
    errors.push("Name cannot have more than 50 characters");
  }

  return errors;
}

export { validateProduct };
