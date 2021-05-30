import http from "../http-common";

const getAll = () => {
  return http.get("/Receipts");
};

const get = id => {
  return http.get(`/Receipts/${id}`);
};

const create = data => {
  return http.post("/Receipts", data);
};

const update = (id, data) => {
  return http.put(`/Receipts/${id}`, data);
};

const remove = id => {
  return http.delete(`/Receipts/${id}`);
};


export default {
  getAll,
  get,
  create,
  update,
  remove,
};

