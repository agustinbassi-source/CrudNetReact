const productId_inputValidation = (fieldValue, rowId: int) => {
  let errors = [];

  return errors;
};

const amount_inputValidation = (fieldValue, rowId: int) => {
  let errors = [];

  return errors;
};

const validateReceiptDetail = (grid) => {
  let errors = [];

  grid.forEach(x => {
    errors.push(...productId_inputValidation(x.productId, x.rowId))
    errors.push(...amount_inputValidation(x.amount, x.rowId))
  });

  return errors;
}

export { validateReceiptDetail };

