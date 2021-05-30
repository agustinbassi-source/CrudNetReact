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
import { validateClient } from '../../services/ValidationClientService'
const ClientEdit = () => {


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

  const { id } = useParams()

  const [model, setModel] = useState({
    "name": null,
  });


  useEffect(() => {
    (async () => {
      const response = await fetch(process.env.REACT_APP_API_URL + `api/Clients/${id}`);
      const data = await response.json();
      console.log(data.data);

      setModel(data.data);
    })();
  }, []);
  const saveChanges = (e) => {

    let errorsArray = [];

    errorsArray.push(...validateClient(model));

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

    const response = fetch(process.env.REACT_APP_API_URL + `api/Clients/${id}`, {
      method: 'PUT', // *GET, POST, PUT, DELETE, etc.
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
          window.location.replace("#/Clients");
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
              Name (columna) (pnl)
&nbsp; {id}            </CCardHeader>
            <CCardBody>
              <CFormGroup row className="my-0">
                <CCol xs="12">
                  <CFormGroup>
                    <CLabel htmlFor="city">Name</CLabel>
                    <CInput  value={model.name || ''} onChange={handleInputChange} id="name" name="name" placeholder="Name"  />
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
    </>
  )
}

export default ClientEdit

