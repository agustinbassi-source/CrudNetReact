import http from "../http-common";

const getAll = () => {
  return http.get("/Clients");
};

const get = id => {
  return http.get(`/Clients/${id}`);
};

const create = data => {
  return http.post("/Clients", data);
};

const update = (id, data) => {
  return http.put(`/Clients/${id}`, data);
};

const remove = id => {
  return http.delete(`/Clients/${id}`);
};


export default {
  getAll,
  get,
  create,
  update,
  remove,
};

