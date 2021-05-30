import React, { useState, useEffect } from "react";
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

export default function FetchData() {

  const [receipt, setReceipt] = useState([]);
  const [loading, setLoading] = useState(true);

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
    setModelFilter({
      ...modelFilter,
      [property]: value
    })
  }

  useEffect(() => {
    (async () => {
      const response = await fetch(process.env.REACT_APP_API_URL + "api/Receipts");
      const data = await response.json();
      console.log(data.data.results);
      setReceipt(data.data.results);
      setLoading(false);
    })();
  }, []);

  const [modalFilter, setModalFilter] = useState(false);

  const [modelFilter, setModelFilter] = useState({ 
  });

  const toggleFilter = () => {
    setModalFilter(!modalFilter);
  }

  const filterAction = (e) => {

    e.preventDefault();

    setLoading(true);

    const response = fetch(process.env.REACT_APP_API_URL + "api/Receipts/GetPage", {
      method: 'POST',
      mode: 'cors',
      cache: 'no-cache',
      credentials: 'same-origin',
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      redirect: 'follow',
      referrerPolicy: 'no-referrer',
      body: JSON.stringify(modelFilter)
    }).then(response => response.json())
      .then(data => {

        setLoading(false);

        console.log(data)

        if (data.data != null && data.data.results != null) {

          setReceipt(data.data.results);
        }
      });

    toggleFilter();
  }

  const remove = (e) => {

    console.log(e);

    let id = parseInt(e.target.name);

    fetch(process.env.REACT_APP_API_URL + "api/Receipts/" + e.target.name, { method: 'DELETE' })
      .then(response => response.json())
      .then(data => {
        setReceipt(receipt.filter(item => item.id !== id));
      });

  }
 
  const edit = (e) => {
    window.location.replace("#/Receipts/Edit/" + e.target.name);
  };

  function renderReceiptTable(receipt) {
    return (
     <>
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Client</th>
          </tr>
        </thead>
        <tbody>
          {receipt.map(item => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.client ? item.client.name : ''}</td>
              <td> <CButton onClick={edit} name={item.id} color="success">Edit</CButton> 
          &nbsp;      <CButton onClick={remove} name={item.id} color="danger">Remove</CButton></td>
            </tr>
          ))}
        </tbody>
      </table>
      <CModal
        show={modalFilter}
        onClose={toggleFilter}
        size="xl"
      >
        <CModalHeader closeButton>Filters</CModalHeader>
        <CModalBody>
              <CFormGroup row className="my-0">
              </CFormGroup>          
        </CModalBody>
        <CModalFooter>
          <CButton onClick={filterAction} color="success">Filter</CButton>
          <CButton
            color="secondary"
            onClick={toggleFilter}
          >Cancel</CButton>
        </CModalFooter>
      </CModal>
     </>
    );
  }
  function renderLoadingMessage() {
    return (<p><em>Loading...</em></p>);
  }
  return (
    <div>
      <h1 id="tableLabel">Receipt items</h1>
 <CButton id="toggle" onClick={toggleFilter} color="primary">Filter</CButton>
      {loading ? renderLoadingMessage() : renderReceiptTable(receipt)}
    </div>
  );
}
