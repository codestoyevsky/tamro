<template>
  <CForm @submit.prevent="checkForm" method="POST">
    <CCard no-header>
      <CCardBody>
        <h3>Add/Edit user</h3>
        <br />
        <CAlert :show.sync="dismissCountDown" :color="alertType" closeButton>
          {{ message }}
          <CProgress
            :max="10"
            :value="dismissCountDown"
            color="success"
            animate
          />
        </CAlert>
        <CRow>
          <CCol col="6">
            <CInput
              label="First Name"
              type="text"
              v-model="user.firstName"
            ></CInput
          ></CCol>
          <CCol col="6">
            <CInput label="Last Name" v-model="user.lastName"></CInput>
          </CCol>

          <CCol col="6">
            <CInput label="Phone Number" v-model="user.phoneNumber"></CInput>
          </CCol>
          <CCol col="6">
            <CInput label="Email" v-model="user.email"></CInput>
          </CCol>
        </CRow>

        <CRow>
          <CCol col="9"></CCol>
          <CCol class="d-flex justify-content-end">
            <CLoadingButton
              :disabled="loading"
              color="primary"
              type="submit"
              class="btn-lg"
              style="margin-right: 15px"
              >Save</CLoadingButton
            >
            <CButton color="secondary" class="btn-lg" @click="goBack"
              >Back</CButton
            >
          </CCol>
        </CRow>
      </CCardBody>
    </CCard>
  </CForm>
</template>

<script>
import axios from "axios";
export default {
  name: "EditUser",
  data: () => {
    return {
      alertType: "success",
      user: {
        firstName: null,
        lastName: null,
        phoneNumber: null,
        email: null,
        id: 0,
      },
      message: "",
      dismissSecs: 4,
      dismissCountDown: 0,
      showDismissibleAlert: false,
    };
  },
  methods: {
    checkForm: function (e) {
      if (this.loading) return;
      this.loading = true;
      if (
        !this.user.email ||
        !this.user.firstName ||
        !this.user.phoneNumber ||
        !this.user.email
      ) {
        this.failed = true;
        this.message = "Please enter all needed fields.";
        this.loading = false;
        this.alertType ="danger";
        this.showAlert();
        return;
      }
      this.save();
    },
    goBack() {
      this.$router.go(-1);
    },
    save() {
      let self = this;
      axios
        .post(this.$apiAdress + "/User", self.user)
        .then((response) => {
          self.user.id = response.data;
          self.message = "Successfully saved user data.";
          self.showAlert();
        })
        .catch(function (error) {
          if (error.response.data.message == "The given data was invalid.") {
            self.message = "";
            for (let key in error.response.data.errors) {
              if (error.response.data.errors.hasOwnProperty(key)) {
                self.message += error.response.data.errors[key][0] + "  ";
              }
            }
            self.showAlert();
          } else {
            console.log(error);
            self.message = error;
            self.showAlert();
          }
        });
    },
    countDownChanged(dismissCountDown) {
      this.dismissCountDown = dismissCountDown;
    },
    showAlert() {
      this.dismissCountDown = this.dismissSecs;
    },
  },
  mounted: function () {
    let self = this;
    if (self.$route.params.id > 0) {
      axios
        .get(this.$apiAdress + "/User/" + self.$route.params.id)
        .then(function (response) {
          self.user = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    }
  },
};
</script>
