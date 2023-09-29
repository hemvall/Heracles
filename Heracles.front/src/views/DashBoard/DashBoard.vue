<template>
    <div class="wholePage">
        <div class="welcomeContainer">
            <h1 class="welcomeTitle">{{ $t('Dashboard.Stats') }}</h1>
            <div class="statsContainer">
                <a class="statBlock">
                    <h2>{{ $t('Dashboard.wThisWeek') }}</h2>
                    <h1 class="statRank" style="color:#9F8548">{{ performancesThisWeek.length }}<br></h1>
                    <h2 class="statLink">> {{ $t('Dashboard.RecordPerformance') }}</h2>

                </a>

                <a class="statBlock">
                    <h2>{{ $t('Dashboard.Rank') }}</h2>
                    <h1 class="statRank" style="color:#9F8548">
                        {{ userRank }}
                    </h1>
                    <h2 class="statLink" @click="$router.push('/leaderboard')">> {{ $t('Dashboard.Leaderboard') }}</h2>

                </a>
                <a class="statBlock">
                    <h2>{{ $t('Dashboard.Score') }}</h2>
                    <h1 class="statRank" style="color:#9F8548">{{ totalScore }}<br></h1>
                </a>
            </div>
        </div>
        <div class="sectionContainer">
            <h1>Goals</h1>
            <div class="goalSection" v-for="g in top3" :key="g.id">
                <h2 class="goalTitle" @click="$router.push(`/goal/${g.id}/detail`)">{{ g.label }}<a
                        v-for="dLeft in daysLeft(g.startingDate, g.deadline)" :key="dLeft"> ({{ dLeft }})</a><br>
                </h2>
                <a>{{ g.startingData }}kg &#10132; {{ g.data }}kg ({{ $t('Dashboard.PR') }}:
                    <a v-for="e in exerciseFromGoal(g.id)" :key="e.id">{{ e.pr }}</a>)
                </a><br>
                <progress value="60" max="100" style="height: 25px;"></progress><a> 60%</a>
            </div>
        </div>
    </div>
    <br /><br />
</template>


<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.idUser = localStorage.getItem('userId')
        this.fetchData();
        console.log(this.userId)
    },
    data() {
        return {
            user: {},
            top3: {},
            totalScore: {},
            leaderboard: [],
            exercises: [],
            performances: [],
            idUser: 0
        };
    },
    methods: {
        fetchData() {
            this.leaderboard = [];
            this.exercises = [];
            this.performances = [];
            this.user = {};
            this.top3 = {};
            this.loading = true;

            fetch(`${this.$api}/leaderboard`)
                .then(r => r.json())
                .then(json => {
                    this.leaderboard = json;
                    this.loading = false;
                    return;
                });

            fetch(`${this.$api}/users/${localStorage.getItem("userId")}`)
                .then(r => r.json())
                .then(json => {
                    this.user = json;
                    this.loading = false;
                    return;
                });

            fetch(`${this.$api}/user/${localStorage.getItem("userId")}/goals/top3`)
                .then(r => r.json())
                .then(json => {
                    this.top3 = json;
                    this.loading = false;
                    return;
                });
            fetch(`${this.$api}/exercises`)
                .then(r => r.json())
                .then(json => {
                    this.exercises = json;
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
            fetch(`${this.$api}/user/${this.idUser}/TotalScore`)
                .then(r => r.json())
                .then(json => {
                    this.totalScore = json;
                    this.loading = false;
                    return;
                });
        },
        exerciseFromGoal(eId) {
            return this.exercises.filter(e => e.id == eId && e.userId == localStorage.getItem('userId'))
        },
        daysLeft(startingDate, deadline) {
            const startDate = new Date(startingDate);
            const endDate = new Date(deadline);
            const currentTime = new Date();

            if (currentTime >= endDate) {
                return ['Goal Expired'];
            }

            const timeDifference = endDate - currentTime;
            const daysLeft = Math.ceil(timeDifference / (24 * 60 * 60 * 1000));

            return [daysLeft + ' days left'];
        },
    },
    computed: {
        typesFromUser() {
            return this.types.filter(e => e.userId == localStorage.getItem('userId'))
        },
        performancesThisWeek() {
            const sevenDaysAgo = new Date();
            sevenDaysAgo.setDate(sevenDaysAgo.getDate() - 7);
            const filteredPerformances = this.performances.filter(p => p.userId == localStorage.getItem('userId') && new Date(p.date) > sevenDaysAgo);
            const uniquePerformances = [];

            for (const performance of filteredPerformances) {
                const exists = uniquePerformances.some(p => new Date(p.date).getTime() === new Date(performance.date).getTime());
                if (!exists) {
                    uniquePerformances.push(performance);
                }
            }

            return uniquePerformances;
        },
        userRank() {
            const userName = localStorage.getItem('userName');
            const userIndex = this.leaderboard.findIndex(u => u == localStorage.getItem('userName'));
            
            if (userIndex >= 0) {
                return userIndex + 1;
            } else {
                return 'N/A';
            }
        },
    }
}
);
</script>

<style scoped lang="scss">
@import url('./DashBoard.scss');

template {
    margin: 0 10%;
}

.wholePage {
    margin-left: 22%;
    margin-top: 8%;
    color: white;
}

* {
    font-family: Arial, Helvetica, sans-serif;
}
</style>