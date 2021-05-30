import React, { useState, useEffect }  from 'react'
import { useParams } from "react-router";
import {
  CButton,
  CCard,
  CCardBody,
  CCardFooter,
  CCardHeader,
  CCol,
  CCollapse,
  CDropdownItem,
  CDropdownMenu,
  CDropdownToggle,
  CFade,
  CForm,
  CFormGroup,
  CFormText,
  CValidFeedback,
  CInvalidFeedback,
  CTextarea,
  CInput,
  CInputFile,
  CInputCheckbox,
  CInputRadio,
  CInputGroup,
  CInputGroupAppend,
  CInputGroupPrepend,
  CDropdown,
  CInputGroupText,
  CLabel,
  CSelect,
  CRow,
  CModal,
  CModalHeader,
  CModalBody,
  CModalFooter
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { validateReceipt } from '../../services/ValidationReceiptService'
import { validateReceiptDetail } from '../../services/ReceiptReceiptDetailService'

const ReceiptCreate = () => {

  const [client, setClient] = useState([{"id": -1, "name": 'Select'}]);

  const [product, setProduct] = useState([{"id": -1, "name": 'Select'}]);

  const [selectedClientId, setSelectedClientId] = useState([{ "id": -1 }]);
  const [selectedProductId, setSelectedProductId] = useState([{ "id": -1 }]);

    const [errors, setErrors] = useState([]);
const renderErrors = () =>
  {
    if (errors.length > 0)
    {
      return (<CRow>
        <CCol xs="12" sm="12">
          <CCard>
            <CCardHeader>
              Errors
            </CCardHeader>
            <CCardBody>
              <CFormGroup row className="my-0">
                <CCol xs="12">
                  <CFormGroup>
                    <table className="table table-striped" aria-labelledby="tableLabel" disabled>
                      <thead>
                        <tr>
                          <th>Descripcion</th>
                        </tr>
                      </thead>
                     <tbody>
                        {errors && errors.map(item => (
                          <tr key={0}>
                            <td>{item}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </CFormGroup>
                </CCol>
              </CFormGroup>          
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>)
  }
}


  const [receiptDetail, setReceiptDetail] = useState({
    "id": 0,
    "rowId": null,
    "productId": -1,
    "amount": null,
  });


  const editReceiptDetail = (e) => {
    let rowId = parseInt(e.target.name);
 
    let filtered = model.receiptDetail.filter(x => x.rowId == rowId);
 
    setReceiptDetail({
      "id": filtered[0].id,
      "rowId": rowId,
      "productId": filtered[0].productId || -1,
      "amount": filtered[0].amount,
    })

    toggleReceiptDetail();
  };

  const removeReceiptDetail = (e) => {
    var rowId = parseInt(e.target.name);

    let filtered = model.receiptDetail.filter(x => {
      return x.rowId != rowId;
    });

    setModel({
      ...model,
      receiptDetail: filtered
    });
  };

  const saveReceiptDetail = (e) => {

    let grid = model.receiptDetail;

    let result = [];

    let tempRow = receiptDetail;

    tempRow.product = ({ "id": receiptDetail.productId, "name": product.filter(x => x.id == receiptDetail.productId)[0].name });

    if (tempRow.productId == -1) {
      tempRow.productId = null;
      tempRow.product = ({ "id": null, "name": null });
    }

    console.log(tempRow);

    if (tempRow.rowId == null) {

      let next = getNextRowId(grid);

      console.log("new")

      tempRow.rowId = next;

      result.push(tempRow);

      result.push(...grid);
    }
    else {

      console.log("edit")

      console.log(grid)

      grid.forEach(function (part, index) {
        if (this[index].rowId == tempRow.rowId) {
          console.log("found!")
          this[index] = tempRow;
        }
      }, grid);

      console.log(grid)

      result.push(...grid);

      console.log("result:")

      console.log(result)

    }

    setModel({
      ...model,
      receiptDetail: result
    });

    toggleReceiptDetail();

    setReceiptDetail({
    "id": 0,
    "rowId": null,
    "productId": -1,
    "amount": null,
    });



  };

  const saveLoteReceiptDetail = (e) => {

    let amountList = getArray(receiptDetail.amount);

    let totalRows = amountList.length;


    let grid = [];

    console.log(grid)

    var next = getNextRowId(model.receiptDetail);

    for (let index = 0; index < totalRows; index++) {


      let tempRow = {
        "id": 0,
        "rowId": next,
        "productId": receiptDetail.productId == -1 ? null : receiptDetail.productId,
        "product": { "id": receiptDetail.productId == -1 ? null : receiptDetail.productId, "name": receiptDetail.productId == -1 ? null : product.filter(x => x.id == receiptDetail.productId)[0].name  },
        "amount": amountList[index],
      };

      console.log(tempRow);

      grid.push(tempRow);

      next++;

    }

    grid.push(...model.receiptDetail)

    setModel({
      ...model,
      receiptDetail: grid
    });

    setReceiptDetail({
    "id": 0,
    "rowId": null,
    "productId": -1,
    "amount": null,

    });

    toggleLoteReceiptDetail();

  };

  const [modalReceiptDetail, setModalReceiptDetail] = useState(false);

  const toggleReceiptDetail = () => {
    setModalReceiptDetail(!modalReceiptDetail);
  };

  const [modalLoteReceiptDetail, setModalLoteReceiptDetail] = useState(false);

  const toggleLoteReceiptDetail = () => {
    setModalLoteReceiptDetail(!modalLoteReceiptDetail);
  };

  const handleInputChangeReceiptDetail = (event, name) => {
    setReceiptDetail({
      ...receiptDetail,
      [name]: event.target.value
    })
  }

  const handleInputIntChangeReceiptDetail = (event, name) => {
    setReceiptDetail({
      ...receiptDetail,
      [name]: parseInt(event.target.value)
    })

    if (name == "productId") {
      setSelectedProductId({ "id": parseInt(event.target.value) });
    }

  }

  const handleCheckboxChangeReceiptDetail = (event) => {
  }

  const getArray = (text) => {

    if (text == null)
      return [];

    return text.replace(/\r\n/g, "\n").split("\n");
  }

  const divModalStyle = {
    "width": "100%", "height": "100%", "overflow": "scroll", "max-height": "700px"
  };

  const textAreaStyle = {
    "width": "200px"
  };

  const getNextRowId = (grid) => {
    if (grid.length == 0)
      return 0;

    return Math.max.apply(Math, grid.map(function (o) { return o.rowId; })) + 1;
  }


  const [model, setModel] = useState({
    "clientId": -1,
    "receiptDetail": [],
  });

  useEffect(() => {
    (async () => {
      const response = await fetch(process.env.REACT_APP_API_URL + "api/Clients");
      const data = await response.json();
      //console.log(data.data.results);
      setClient([...client, ...data.data.results]);
    })();
  }, []);

  useEffect(() => {
    (async () => {
      const response = await fetch(process.env.REACT_APP_API_URL + "api/Products");
      const data = await response.json();
      //console.log(data.data.results);
      setProduct([...product, ...data.data.results]);
    })();
  }, []);

  const saveChanges = (e) => {

    let errorsArray = [];

    errorsArray.push(...validateReceipt(model));

    errorsArray.push(...validateReceiptDetail(model.receiptDetail));

    if (errorsArray.length > 0)
    {
      setErrors([...errorsArray]);
      return;
    }
    else
    {
      setErrors([]);
    }

    //  setShowLoading(true);
    e.preventDefault();

    const response = fetch(process.env.REACT_APP_API_URL + "api/Receipts", {
      method: 'POST', // *GET, POST, PUT, DELETE, etc.
      mode: 'cors', // no-cors, *cors, same-origin
      cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      credentials: 'same-origin', // include, *same-origin, omit
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      redirect: 'follow', // manual, *follow, error
      referrerPolicy: 'no-referrer', // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
      body: JSON.stringify(model) // body data type must match "Content-Type" header
    }).then(response => response.json())
      .then(data => {
        console.log(data)
        if (data.data != null && data.data != 0) {
          // alert(data.data);
          //  history.push("/#/BankAccount");
          window.location.replace("#/Receipts");
        }
      });
  };

  const handleSelectIntChange = (event) => {
    setModel({
      ...model,
      [event.target.name + "Id"]: parseInt(event.target.value),
    })
  }

  const handleInputChange = (event) => {
    updateState(event.target.name, event.target.value)
  }

  const handleInputIntChange = (event) => {
    updateState(event.target.name, parseInt(event.target.value))
  }

  const handleCheckboxChange = (event) => {
    updateState(event.target.name, event.target.checked)
  }

  const updateState = (property, value) => {
    setModel({
      ...model,
      [property]: value
    })
  }


  return (
    <>
      <CRow>
        <CCol xs="12" sm="12">
          <CCard>
            <CCardHeader>
              Client (columna) (pnl)
            </CCardHeader>
            <CCardBody>
              <CFormGroup row className="my-0">
                <CCol xs="12">
                  <CFormGroup>
                    <CLabel htmlFor="city">Client</CLabel>
                                        <CSelect  value={model.clientId}  custom name="client" id="client" onChange={handleSelectIntChange}>
                      {client.map(item => (
                        <option
                          key={item.id}
                          value={item.id}
                        >
                          {item.name}
                        </option>
                      ))}
                    </CSelect>
                  </CFormGroup>
                </CCol>
              </CFormGroup>          
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
      <CRow>
        <CCol xs="12" sm="12">
          <CCard>
            <CCardHeader>
              Receipt Detail (columna) (pnl)
            </CCardHeader>
            <CCardBody>
              <CFormGroup row className="my-0">
                <CCol xs="12">
                  <CFormGroup>
                          <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Product</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
          {model.receiptDetail && model.receiptDetail.map(item => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.product ? item.product.name : ''}</td>
              <td>{item.amount}</td>
              <td> <CButton onClick={editReceiptDetail} name={item.rowId} color="success">Edit</CButton> 
          &nbsp;      <CButton onClick={removeReceiptDetail} name={item.rowId} color="danger">Remove</CButton></td>
            </tr>
          ))}
        </tbody>
      </table>
                  </CFormGroup>
                </CCol>
                <CCol xs="12">
                  <CFormGroup>
                    <CButton onClick={toggleReceiptDetail} color="primary">Add ReceiptDetail</CButton>
                 &nbsp;   <CButton onClick={toggleLoteReceiptDetail} color="primary">Add lote ReceiptDetail</CButton>
                  </CFormGroup>
                </CCol>
              </CFormGroup>          
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
      {renderErrors()}      <CRow>
        <CCol xs="12" sm="12">
          <CCard>
            <CCardHeader>
              Actions
            </CCardHeader>
            <CCardBody>
              <CFormGroup row className="my-0">
                <CCol xs="12">
                  <CFormGroup>                  
                    <CButton onClick={saveChanges} color="primary">Save changes</CButton>
                  &nbsp;  <CButton color="secondary">Cancel</CButton>
                  </CFormGroup>
                </CCol>
              </CFormGroup>          
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>

      <CModal
        show={modalLoteReceiptDetail}
        onClose={toggleLoteReceiptDetail}
        size="xl"
      >
        <CModalHeader closeButton>ReceiptDetail Lote</CModalHeader>
        <CModalBody>
              <CFormGroup row className="my-0">
            <div style={divModalStyle}>
              <table>
                <tr>
                  <td valign="top">                     <CSelect   style={textAreaStyle}  value={receiptDetail.productId || -1} custom name="receiptDetail_ProductId" id="receiptDetail_ProductId" onChange={(e) => handleInputIntChangeReceiptDetail(e, "productId")}>
                      {product.map(item => (
                        <option
                          key={item.id}
                          value={item.id}
                        >
                          {item.name}
                        </option>
                      ))}
                    </CSelect>
 </td>
                  <td valign="top"> <CTextarea type="number"  style={textAreaStyle}   rows="520"  value={receiptDetail.amount || ''} onChange={(e) => handleInputIntChangeReceiptDetail(e, "amount")} id="receiptDetail_Amount" name="receiptDetail_Amount" placeholder="Amount" ></CTextarea>
 </td>
                </tr>
              </table>
            </div>
              </CFormGroup>          
        </CModalBody>
        <CModalFooter>
          <CButton onClick={saveLoteReceiptDetail} color="success">Save Lote</CButton>
          <CButton
            color="secondary"
            onClick={toggleLoteReceiptDetail}
          >Cancel</CButton>
        </CModalFooter>
      </CModal>

<CInput type="number" hidden value={0}  id="ReceiptDetailId" name="ReceiptDetail"  />
      <CModal
        show={modalReceiptDetail}
        onClose={toggleReceiptDetail}
        size="xl"
      >
        <CModalHeader closeButton>ReceiptDetail </CModalHeader>
        <CModalBody>
              <CFormGroup row className="my-0">
                <CCol   xs="6" >
                  <CFormGroup>
                    <CLabel htmlFor="city">Product</CLabel>
                                        <CSelect   value={receiptDetail.productId || -1} custom name="receiptDetail_ProductId" id="receiptDetail_ProductId" onChange={(e) => handleInputIntChangeReceiptDetail(e, "productId")}>
                      {product.map(item => (
                        <option
                          key={item.id}
                          value={item.id}
                        >
                          {item.name}
                        </option>
                      ))}
                    </CSelect>
                  </CFormGroup>
                </CCol>
                <CCol   xs="6" >
                  <CFormGroup>
                    <CLabel htmlFor="city">Amount</CLabel>
                    <CInput type="number"   value={receiptDetail.amount || ''} onChange={(e) => handleInputIntChangeReceiptDetail(e, "amount")} id="receiptDetail_Amount" name="receiptDetail_Amount" placeholder="Amount"  />
                  </CFormGroup>
                </CCol>
              </CFormGroup>          
        </CModalBody>
        <CModalFooter>
          <CButton onClick={saveReceiptDetail} color="success">Save </CButton>
          <CButton
            color="secondary"
            onClick={toggleReceiptDetail}
          >Cancel</CButton>
        </CModalFooter>
      </CModal>
    </>
  )
}

export default ReceiptCreate

