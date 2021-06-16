<script>
import store from "../store";

export default {
  name: "nav",
  data() {
    return {
      result: [],
    };
  },
  computed: {
    sidebarItems() {
      return this.result;
    },

    getInstance() {
      return store.state.instance;
    },
  },
  methods: {
    rebuildData() {
      let self = this;
      self.result = [
        {
          _name: "CSidebarNav",
          _children: [
            {
              _name: "CSidebarNavItem",
              name: this.$t("menu.dashboard"),
              to: "/dashboard",
              icon: "cil-speedometer",
            },
            {
              _name: "CSidebarNavItem",
              name: this.$t("menu.users"),
              to: "/users",
              icon: "cil-group",
            },
          ],
        },
      ];
    },
  },
  mounted: function () {
    let self = this;
    let result = self.rebuildData();
  },
};
</script>
