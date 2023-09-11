<template>
    <div class="container" v-if="formPerformanceOpen">
        <div class="form">
            <h2>Add a performance</h2>
            <form>
                <p>Date</p>
                <input v-model="perfDate" type="date" />
                <p>Performance</p>
                <input v-model="Weight" type="number" /> <a>{{ exercise.unit }}</a>
                <p>Reps</p>
                <input v-model="Reps" type="number" />
                <p>Sets</p>
                <input v-model="Sets" type="number" /><br><br>
                <button @click="createPerformance" class="formConfirm">Add</button>
                <button @click="formPerformanceOpen = !formPerformanceOpen" class="formReturn">Close</button>
            </form>
        </div>
    </div>
    <div class="wholePage">
        <a class="returnButton" @click="$router.go(-1)">Return</a>
        <div class="typeBlock">
            <h1>{{ exercise.label }}</h1>
            <a class="button" style="margin-left: 0%;" @click="formPerformanceOpen = !formPerformanceOpen">Add a
                performance</a>
            <table>
                <thead>
                    <th>Date</th>
                    <th>Performance</th>
                    <th>Reps</th>
                    <th>Sets</th>
                    <th>Total Score</th>
                </thead>
                <tbody v-if="perfFromExercise(this.$route.params.exerciseId).length > 0">
                    <tr v-for="performance in perfFromExercise(this.$route.params.exerciseId)" :key="performance.id">
                        <td>{{ performance.date }}</td>
                        <td>{{ performance.weight }} {{ exercise.unit }}</td>
                        <td>{{ performance.reps }}</td>
                        <td>{{ performance.sets }}</td>
                        <td>{{ performance.score }}</td>
                    </tr>
                </tbody>
                <a v-else><br />Aucune performance n'a chargée.</a>
            </table>

        </div>
    </div>
</template>


<script lang="js">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
    created() {
        this.fetchData();
    },
    data() {
        return {
            exercise: {},
            performances: [],
            formPerformanceOpen: false,
            editPerfOpen: false,
            UserId: 1,
            perfDate: new Date().toISOString(),
            Weight: 0,
            Reps: 0,
            Sets: 0,
            Score: 0,
        };
    },
    methods: {
        fetchData() {
            this.exercise = {};
            this.performances = [];
            this.loading = true;

            fetch(`${this.$api}/exercises/${this.$route.params.exerciseId}`)
                .then(r => r.json())
                .then(json => {
                    this.exercise = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/performances`)
                .then(r => r.json())
                .then(json => {
                    this.performances = json;
                    this.loading = false;
                    return;
                });
        },
        goBack() {
            const router = useRouter();
            router.go(-1);
        },
        createPerformance() {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    ExerciseId: this.$route.params.exerciseId,
                    UserId: this.UserId,
                    Date: this.perfDate,
                    Weight: this.Weight,
                    Reps: this.Reps,
                    Sets: this.Sets,
                    Score: this.Score
                })
            };
            fetch(`${this.$api}/performances`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La performance a bien été ajoutée"); this.formPerformanceOpen = false }
                    else { alert("Erreur. La performance n'a pas été ajoutée") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        },
        editPerformance(pId) {
            const requestOptions = {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    Id: pId,
                    ExerciseId: this.$route.params.exerciseId,
                    UserId: this.UserId,
                    Date: this.perfDate,
                    Weight: this.Weight,
                    Reps: this.Reps,
                    Sets: this.Sets,
                    Score: this.Score
                })
            };
            fetch(`${this.$api}/performances/${pId}`, requestOptions)
                .then(response => {
                    if (response.ok) { alert("La performance a bien été modifiée") }
                    else { alert("La performance a été modifiée") }
                    this.fetchData()
                })
                .then(response => response.json())
            // .then(data => (this.postId = data.id));
        },
        perfFromExercise(eId) {
            return this.performances.filter(p => p.exerciseId == eId && p.userId == localStorage.getItem('userId'))
        }
    }
}
);
</script>

<style scoped lang="scss">
@import url('./SpecificExercise.scss');

template {
    margin: 0 10%;
}

* {
    color: white;
}

input,
button {
    color: black;
}


.container {
   display: flex;
   justify-content: center;
}

.form {
    position: fixed;
    z-index: 10;
    background-color: #141010;
    height: 70%;
    width: 70%;
    padding: 1% 8%;
    border: 1px solid #ccc;
    border-radius: 5px;
    box-shadow: 0 2px 4px #0000001a;
}
.form input[type="text"],
.form input[type="password"],
.form button {
    display: block;
    margin-bottom: 10px;
    width: 100%;
    padding: 10px;
}
</style>