<template>
  <transition name="slide">
    <CCard>
      <CCardBody>
        <CButton color="primary" @click="add()"
          ><CIcon name="cil-plus" /> Add new user</CButton
        >
        <br />
        <br />
        <CAlert :show.sync="dismissCountDown" color="primary" fade>
          {{ message }}</CAlert
        >
        <CDataTable
          :items="items"
          :fields="fields"
          table-filter
          items-per-page-select
          :items-per-page="15"
          hover
          pagination
        >
          <template #firstname="{ item }">
            <td>
              <strong>{{ item.firstName }}</strong>
            </td>
          </template>
          <template #lastname="{ item }">
            <td>
              <strong>{{ item.lastName }}</strong>
            </td>
          </template>
          <template #phonenumber="{ item }">
            <td>
              <strong>{{ item.phoneNumber }}</strong>
            </td>
          </template>

          <template #email="{ item }">
            <td>
              <strong>{{ item.email }}</strong>
            </td>
          </template>

          <template #edit="{ item }">
            <td style="width: 15%">
              <CButton color="primary" @click="edit(item.id)"
                ><CIcon name="cil-pencil" /> Edit</CButton
              >
            </td>
          </template>
          <template #delete="{ item }">
            <td style="width: 15%">
              <CButton
                color="danger"
                @click="
                  warningModal = true;
                  deleteId = item.id;
                  deletedItem = item;
                "
                ><CIcon name="cil-trash" /> Delete</CButton
              >
            </td>
          </template>
        </CDataTable>

        <CModal
          v-model="deletedItem.id"
          title="Are you sure to delete selected user?"
          color="danger"
          :show.sync="warningModal"
          @update:show="closeModal"
        >
          <CRow>
            <CCol col="6">
              <CInput label="First Name" v-model="deletedItem.firstName" disabled />
            </CCol>
            <CCol col="6">
              <CInput label="Last Name" v-model="deletedItem.lastName" disabled />
            </CCol>

            <CCol col="6">
              <CInput label="Phone Number" v-model="deletedItem.phoneNumber" disabled />
            </CCol>

            <CCol col="6">
              <CInput label="Email" v-model="deletedItem.email" disabled />
            </CCol>
          </CRow>
        </CModal>
      </CCardBody>
    </CCard>
  </transition>
</template>

<script>
import axios from "axios";

export default {
  name: "Users",
  data: () => {
    return {
      deletedItem: [],
      warningModal: false,
      items: [],
      fields: [
        "firstName",
        "lastName",
        "phoneNumber",
        "email",
        "edit",
        "delete",
      ],
      currentPage: 1,
      perPage: 5,
      totalRows: 0,
      message: null,
      showMessage: false,
      dismissSecs: 3,
      dismissCountDown: 0,
      showDismissibleAlert: false,
    };
  },
  computed: {},
  methods: {
    closeModal(status, evt, accept) {
      if (accept) {
        this.delete(this.deleteId);
      }
    },
    getRowCount(items) {
      return items.length;
    },
    
    editLink(id) {
      return `users/${id.toString()}/create`;
    },
    edit(id) {
      const editLink = this.editLink(id);
      this.$router.push({ path: editLink });
    },
    delete(id) {
      let self = this;
      axios
        .delete(this.$apiAdress + "/User/" + id)

        .then(function () {
          self.message = "Successfully deleted selected user.";
          self.showAlert();
          self.loadUsers();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    add() {
      this.$router.push({ path: "users/0/create" });
    },
    countDownChanged(dismissCountDown) {
      this.dismissCountDown = dismissCountDown;
    },
    showAlert() {
      this.dismissCountDown = this.dismissSecs;
    },
    loadUsers() {
      let self = this;
      axios
        .get(this.$apiAdress + "/User")
        .then(function (response) {
          self.items = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
  mounted: function () {
    this.loadUsers();
  },
};
</script>

<style scoped>
.card-body >>> table > tbody > tr > td {
  cursor: pointer;
}
</style>
